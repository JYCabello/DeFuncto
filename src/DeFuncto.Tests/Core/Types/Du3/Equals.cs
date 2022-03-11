﻿using DeFuncto.Extensions;
using FsCheck;
using FsCheck.Xunit;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.Du3;

public class Equals
{
    [Property(DisplayName = "Is not equal to null")]
    public void FalseByNull(NonNull<string> a) =>
        First<string, string, string>(a.Get)
            .Equals((object)null)
            .Run(result => Assert.False(result));

    [Property(DisplayName = "First is not equal to First")]
    public void FirstNotEqualToFirst(NonNull<string> a) =>
        First<string, string, string>($"a{a.Get}")
            .Equals(First<string, string, string>($"b{a.Get}"))
            .Run(result => Assert.False(result));

    [Property(DisplayName = "First is not equal to Second")]
    public void FirstNotEqualToSecond(NonNull<string> a, NonNull<string> b) =>
        First<string, string, string>(a.Get)
            .Equals(Second<string, string, string>(b.Get))
            .Run(result => Assert.False(result));

    [Property(DisplayName = "First is not equal to Third")]
    public void FirstNotEqualToThird(NonNull<string> a, NonNull<string> b) =>
        First<string, string, string>(a.Get)
            .Equals(Third<string, string, string>(b.Get))
            .Run(result => Assert.False(result));

    [Property(DisplayName = "Second is not equal to Second")]
    public void SecondNotEqualToSecond(NonNull<string> a) =>
        Second<string, string, string>($"a{a.Get}")
            .Equals(Second<string, string, string>($"b{a.Get}"))
            .Run(result => Assert.False(result));

    [Property(DisplayName = "Second is not equal to Third")]
    public void SecondNotEqualToThird(NonNull<string> a, NonNull<string> b) =>
        Second<string, string, string>(a.Get)
            .Equals(Third<string, string, string>(b.Get))
            .Run(result => Assert.False(result));

    [Property(DisplayName = "Third is not equal to Third")]
    public void ThirdNotEqualToThird(NonNull<string> a) =>
        Third<string, string, string>($"a{a.Get}")
            .Equals(Third<string, string, string>($"b{a.Get}"))
            .Run(result => Assert.False(result));

    [Property(DisplayName = "First is equal to First")]
    public void FirstEqualToFirst(NonNull<string> a) =>
        First<string, string, string>(a.Get)
            .Equals(First<string, string, string>(a.Get))
            .Run(result => Assert.True(result));

    [Property(DisplayName = "Second is equal to Second")]
    public void SecondEqualToSecond(NonNull<string> a) =>
        Second<string, string, string>(a.Get)
            .Equals(Second<string, string, string>(a.Get))
            .Run(result => Assert.True(result));

    [Property(DisplayName = "Third is equal to Third")]
    public void ThirdEqualToThird(NonNull<string> a) =>
        Third<string, string, string>(a.Get)
            .Equals(Third<string, string, string>(a.Get))
            .Run(result => Assert.True(result));
}
