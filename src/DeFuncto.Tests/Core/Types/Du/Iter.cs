﻿using DeFuncto.Assertions;
using FsCheck;
using FsCheck.Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.Du
{
    public class Iter
    {
        [Property(DisplayName = "Does not run for T1 when it's T2")]
        public void SkipsT1WhenT2(int a)
        {
            var du1Witness = new Witness();
            var du2Witness = new Witness();

            new Du<string, int>(a)
                .Iter(
                    (string _) => { du1Witness.Touch(); },
                    (int _) => { du2Witness.Touch(); }
                );

            du1Witness.ShouldHaveBeenTouched(0);
            du2Witness.ShouldHaveBeenTouched(1);
        }

        [Property(DisplayName = "Does not run for T2 when it's T1")]
        public void SkipsT2WhenT1(int a)
        {
            var du1Witness = new Witness();
            var du2Witness = new Witness();

            new Du<int, string>(a)
                .Iter(
                    (int _) => { du1Witness.Touch(); },
                    (string _) => { du2Witness.Touch(); }
                );

            du1Witness.ShouldHaveBeenTouched(1);
            du2Witness.ShouldHaveBeenTouched(0);
        }

        //[Property(DisplayName = "Does not run for T1 when it's T2")]
        //public void SkipsT1WhenT2(int a)
        //{
        //    var witness = new Witness();

        //    new Du<string, int>(a)
        //        .Iter((string _) => { witness.Touch(); });

        //    witness.ShouldHaveBeenTouched(0);
        //}

        //[Property(DisplayName = "Does not run for T2 when it's T1")]
        //public void SkipsT2WhenT1(int a)
        //{
        //    var witness = new Witness();

        //    new Du<int, string>(a)
        //        .Iter((string _) => { witness.Touch(); });

        //    witness.ShouldHaveBeenTouched(0);
        //}

        //[Property]
        //public void OnDu1Action(NonNull<string> a)
        //{
        //    var witness = new Witness();

        //    new Du<string, int>(a.Get)
        //        .Iter((string _) => { witness.Touch(); });

        //    witness.ShouldHaveBeenTouched(1);
        //}

        //[Property]
        //public void OnDu1Func(NonNull<string> a)
        //{
        //    var witness = new Witness();

        //    new Du<string, int>(a.Get)
        //        .Iter(_ =>
        //        {
        //            witness.Touch();
        //            return unit;
        //        });

        //    witness.ShouldHaveBeenTouched(1);
        //}

        //[Property]
        //public void OnDu2Action(int a)
        //{
        //    var witness = new Witness();

        //    new Du<string, int>(a)
        //        .Iter((int _) => { witness.Touch(); });

        //    witness.ShouldHaveBeenTouched(1);
        //}
    }
}
