using System;
using System.Threading;

namespace DeFuncto.Assertions;

public class ConcurrentWitness
{
    private readonly SemaphoreSlim semaphore = new(1);
    private int currentHoldCount;
    public int MaxConcurrentHolds { get; private set; }

    public int TimesCalled { get; private set; }

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

    public ConcurrentWitness ShouldBeenHeldMax(int max)
    {
        if (MaxConcurrentHolds > max)
            throw new AssertionFailedException($"It was expected to be held a maximum of {max} times at the same time but it was {MaxConcurrentHolds}");
        return this;
    }

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
            Dispose(true);

        // ReSharper disable once UnusedParameter.Local
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                witness.Release();
                GC.SuppressFinalize(this);
            }
        }

        ~Holder() =>
            witness.Release();
    }
}
