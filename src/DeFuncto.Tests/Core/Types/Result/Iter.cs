﻿using System;
using DeFuncto.Assertions;
using FsCheck;
using FsCheck.Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.Result;

public class Iter
{
    [Property(DisplayName = "Iterates on Ok")]
    public void OnOk(NonNull<string> a)
    {
        var witness = new Witness();
        Ok(a.Get).Result<int>()
            .Iter((string _) =>
            {
                witness.Touch();
            });

        Ok(a.Get).Result<int>().Iter((string _) =>
        {
            witness.Touch();
            return unit;
        });

        Ok(a.Get).Result<int>().Iter(_ =>
            {
                witness.Touch();
                return unit;
            },
            _ => throw new Exception("Should not run"));

        witness.ShouldHaveBeenTouched(3);
    }

    [Property(DisplayName = "Iterates on Error")]
    public void OnError(NonNull<string> a)
    {
        var witness = new Witness();
        Error(a.Get).Result<int>().Iter((string _) => { witness.Touch(); });
        Error(a.Get).Result<int>().Iter((string _) =>
        {
            witness.Touch();
            return unit;
        });
        Error(a.Get).Result<int>().Iter(
            _ => throw new Exception("Should not run"),
            _ =>
            {
                witness.Touch();
                return unit;
            });
        Error(a.Get).Result<int>().Iter(_ => throw new Exception("Should not run"), _ => { witness.Touch(); });
        witness.ShouldHaveBeenTouched(4);
    }

    [Property(DisplayName = "Skips Error iter on Ok")]
    public void OnOkSkipError(NonNull<string> a)
    {
        var witness = new Witness();
        Ok(a.Get).Result<int>().Iter((string _) => { witness.Touch(); });
        Ok(a.Get).Result<int>().Iter((string _) =>
        {
            witness.Touch();
            return unit;
        });
        Ok(a.Get).Result<int>().Iter(
            _ =>
            {
                witness.Touch();
                return unit;
            },
            _ => throw new Exception("Should not run"));
        Ok(a.Get).Result<int>().Iter(_ => { witness.Touch(); }, _ => throw new Exception("Should not run"));
        witness.ShouldHaveBeenTouched(4);
    }
}
