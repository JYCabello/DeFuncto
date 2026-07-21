using System;
using System.Threading;

namespace DeFuncto.Assertions;

/// <summary>
/// Test helper that tracks concurrent access, recording the maximum number of simultaneous holds
/// and the total number of times it has been held.
/// </summary>
public class ConcurrentWitness
{
    private readonly SemaphoreSlim semaphore = new(1);
    private int currentHoldCount;
    /// <summary>
    /// Highest number of holds observed to be active at the same time.
    /// </summary>
    public int MaxConcurrentHolds { get; private set; }

    /// <summary>
    /// Total number of times a hold has been taken and released.
    /// </summary>
    public int TimesCalled { get; private set; }

    /// <summary>
    /// Takes a hold on the witness for the lifetime of the returned disposable.
    /// </summary>
    /// <returns>A disposable that releases the hold when disposed.</returns>
    public IDisposable Grab() =>
        new Holder(this);

    private void Hold()
    {
        semaphore.Wait();
        currentHoldCount++;
        MaxConcurrentHolds = Math.Max(MaxConcurrentHolds, currentHoldCount);
        semaphore.Release();
    }

    private void Release()
    {
        semaphore.Wait();
        TimesCalled++;
        currentHoldCount--;
        semaphore.Release();
    }

    /// <summary>
    /// Asserts that the witness was never held by more than the given number of holders at the same time.
    /// </summary>
    /// <param name="max">Maximum number of concurrent holds allowed.</param>
    /// <returns>This instance.</returns>
    public ConcurrentWitness ShouldBeenHeldMax(int max)
    {
        if (MaxConcurrentHolds > max)
            throw new AssertionFailedException($"It was expected to be held a maximum of {max} times at the same time but it was {MaxConcurrentHolds}");
        return this;
    }

    /// <summary>
    /// Asserts that the witness was held the given total number of times.
    /// </summary>
    /// <param name="total">Expected total number of holds.</param>
    /// <returns>This instance.</returns>
    public ConcurrentWitness ShouldBeenHeldTotal(int total)
    {
        if (total != TimesCalled)
            throw new AssertionFailedException($"It was expected to be requested {total} times but it was {TimesCalled}");
        return this;
    }

    private sealed class Holder : IDisposable
    {
        private readonly ConcurrentWitness witness;

        public Holder(ConcurrentWitness witness)
        {
            this.witness = witness;
            this.witness.Hold();
        }

        public void Dispose() =>
            witness.Release();
    }
}
