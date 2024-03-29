﻿using DeFuncto.Extensions;
using FsCheck;
using FsCheck.Xunit;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.Result;

public class Equals
{
    [Property(DisplayName = "Is not equal to null")]
    public void FalseByNull(NonNull<string> a) =>
        Ok<string, int>(a.Get)
            .Equals((object)null!)
            .Run(Assert.False);

    [Property(DisplayName = "Ok is not equal to Error")]
    public void OkNotEqualToError(NonNull<string> a, int b) =>
        Ok<string, int>(a.Get)
            .Equals(Error<string, int>(b))
            .Run(Assert.False);

    [Property(DisplayName = "Error is not equal to Ok")]
    public void ErrorNotEqualToOk(NonNull<string> a, int b) =>
        Error<string, int>(b)
            .Equals(Ok<string, int>(a.Get))
            .Run(Assert.False);

    [Property(DisplayName = "Ok are not the same")]
    public void OkAreNotTheSame(NonNull<string> a) =>
        Ok<string, int>($"a{a.Get}")
            .Equals(Ok<string, int>($"b{a.Get}"))
            .Run(Assert.False);

    [Property(DisplayName = "Error are not the same")]
    public void ErrorAreNotTheSame(NonNull<string> a) =>
        Error<int, string>($"a{a.Get}")
            .Equals(Error<int, string>($"b{a.Get}"))
            .Run(Assert.False);

    [Property(DisplayName = "Ok are equal")]
    public void OkEqual(NonNull<string> a) =>
        Ok<string, int>(a.Get)
            .Equals(Ok<string, int>(a.Get))
            .Run(Assert.True);

    [Property(DisplayName = "Error are equal")]
    public void ErrorEqual(int a) =>
        Error<string, int>(a)
            .Equals(Error<string, int>(a))
            .Run(Assert.True);
}
