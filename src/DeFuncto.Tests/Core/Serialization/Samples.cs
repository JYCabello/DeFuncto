namespace DeFuncto.Tests.Core.Serialization;

public static class Samples
{
  public static readonly TheoryData<object[]> Data =
  [
    new object[] { Option<int>.None },
    new object[] { Option<int>.Some(1) },
    new object[] { Result<int, string>.Ok(1) },
    new object[] { Result<int, string>.Error("error") },
    new object[] { Du<int, int>.First(10) },
    new object[] { Du<int, int>.Second(20) },
    new object[] { Du3<int, int, int>.First(10) },
    new object[] { Du3<int, int, int>.Second(20) },
    new object[] { Du3<int, int, int>.Third(30) },
    new object[] { Du4<int, int, int, int>.First(10) },
    new object[] { Du4<int, int, int, int>.Second(20) },
    new object[] { Du4<int, int, int, int>.Third(30) },
    new object[] { Du4<int, int, int, int>.Fourth(40) },
    new object[] { Du5<int, int, int, int, int>.First(10) },
    new object[] { Du5<int, int, int, int, int>.Second(20) },
    new object[] { Du5<int, int, int, int, int>.Third(30) },
    new object[] { Du5<int, int, int, int, int>.Fourth(40) },
    new object[] { Du5<int, int, int, int, int>.Fifth(50) },
    new object[] { Du6<int, int, int, int, int, int>.First(10) },
    new object[] { Du6<int, int, int, int, int, int>.Second(20) },
    new object[] { Du6<int, int, int, int, int, int>.Third(30) },
    new object[] { Du6<int, int, int, int, int, int>.Fourth(40) },
    new object[] { Du6<int, int, int, int, int, int>.Fifth(50) },
    new object[] { Du6<int, int, int, int, int, int>.Sixth(60) },
    new object[] { Du7<int, int, int, int, int, int, int>.First(10) },
    new object[] { Du7<int, int, int, int, int, int, int>.Second(20) },
    new object[] { Du7<int, int, int, int, int, int, int>.Third(30) },
    new object[] { Du7<int, int, int, int, int, int, int>.Fourth(40) },
    new object[] { Du7<int, int, int, int, int, int, int>.Fifth(50) },
    new object[] { Du7<int, int, int, int, int, int, int>.Sixth(60) },
    new object[] { Du7<int, int, int, int, int, int, int>.Seventh(70) }
  ];
}
