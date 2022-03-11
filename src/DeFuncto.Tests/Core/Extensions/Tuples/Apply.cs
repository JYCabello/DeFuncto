using DeFuncto.Extensions;
using FsCheck.Xunit;
using Xunit;

namespace DeFuncto.Tests.Core.Extensions.Tuples;

public class Apply
{
    [Property(DisplayName = "Applies to tuple")]
    public void Tuple(string iA, string iB) =>
        (iA, iB)
        .Apply((a, b) => a + b)
        .Run(result => Assert.Equal(iA + iB, result));

    [Property(DisplayName = "Applies to triple")]
    public void Triple(string iA, string iB, string iC) =>
        (iA, iB, iC)
        .Apply((a, b, c) => a + b + c)
        .Run(result => Assert.Equal(iA + iB + iC, result));

    [Property(DisplayName = "Applies to quadruple")]
    public void Quadruple(string iA, string iB, string iC, string iD) =>
        (iA, iB, iC, iD)
        .Apply((a, b, c, d) => a + b + c + d)
        .Run(result => Assert.Equal(iA + iB + iC + iD, result));

    [Property(DisplayName = "Applies to quintuple")]
    public void Quintuple(string iA, string iB, string iC, string iD, string iE) =>
        (iA, iB, iC, iD, iE)
        .Apply((a, b, c, d, e) => a + b + c + d + e)
        .Run(result => Assert.Equal(iA + iB + iC + iD + iE, result));

    [Property(DisplayName = "Applies to sextuple")]
    public void Sextuple(string iA, string iB, string iC, string iD, string iE, string iF) =>
        (iA, iB, iC, iD, iE, iF)
        .Apply((a, b, c, d, e, f) => a + b + c + d + e + f)
        .Run(result => Assert.Equal(iA + iB + iC + iD + iE + iF, result));
}
