using static DeFuncto.Prelude;

namespace DeFuncto.Assertions;

/// <summary>
/// Test helper that records whether and how many times it was touched.
/// </summary>
public class Witness
{
    /// <summary>
    /// The number of times this witness has been touched.
    /// </summary>
    public int TimesCalled { get; private set; }

    /// <summary>
    /// Records a touch by incrementing the touch count.
    /// </summary>
    /// <returns>Unit.</returns>
    public Unit Touch()
    {
        TimesCalled++;
        return unit;
    }
}
