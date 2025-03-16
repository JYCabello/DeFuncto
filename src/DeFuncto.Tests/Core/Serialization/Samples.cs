namespace DeFuncto.Tests.Core.Serialization;

public static class Samples
{
  public static readonly TheoryData<object[]> Data =
  [
    [Option<int>.None],
    [Option<int>.Some(1)],
    [Result<int, string>.Ok(1)],
    [Result<int, string>.Error("error")],
    [Du<int, int>.First(10)],
    [Du<int, int>.Second(20)],
    [Du3<int, int, int>.First(10)],
    [Du3<int, int, int>.Second(20)],
    [Du3<int, int, int>.Third(30)],
    [Du4<int, int, int, int>.First(10)],
    [Du4<int, int, int, int>.Second(20)],
    [Du4<int, int, int, int>.Third(30)],
    [Du4<int, int, int, int>.Fourth(40)],
    [Du5<int, int, int, int, int>.First(10)],
    [Du5<int, int, int, int, int>.Second(20)],
    [Du5<int, int, int, int, int>.Third(30)],
    [Du5<int, int, int, int, int>.Fourth(40)],
    [Du5<int, int, int, int, int>.Fifth(50)],
    [Du6<int, int, int, int, int, int>.First(10)],
    [Du6<int, int, int, int, int, int>.Second(20)],
    [Du6<int, int, int, int, int, int>.Third(30)],
    [Du6<int, int, int, int, int, int>.Fourth(40)],
    [Du6<int, int, int, int, int, int>.Fifth(50)],
    [Du6<int, int, int, int, int, int>.Sixth(60)],
    [Du7<int, int, int, int, int, int, int>.First(10)],
    [Du7<int, int, int, int, int, int, int>.Second(20)],
    [Du7<int, int, int, int, int, int, int>.Third(30)],
    [Du7<int, int, int, int, int, int, int>.Fourth(40)],
    [Du7<int, int, int, int, int, int, int>.Fifth(50)],
    [Du7<int, int, int, int, int, int, int>.Sixth(60)],
    [Du7<int, int, int, int, int, int, int>.Seventh(70)]
  ];
}
