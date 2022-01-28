using DeFuncto.Assertions;
using FsCheck;
using FsCheck.Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.Du
{
    public class Iter
    {
        [Property(DisplayName = "Runs T1 when it is T1")]
        public void RunsT1(int a)
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

        [Property(DisplayName = "Runs T2 when it is T2")]
        public void RunsT2(int a)
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

        [Property(DisplayName = "Runs T1 when it is T1")]
        public void RunsT1IterFuncs(int a)
        {
            var du1Witness = new Witness();
            var du2Witness = new Witness();

            new Du<int, string>(a)
                .Iter(
                    (int _) => { du1Witness.Touch(); return unit; },
                    (string _) => { du2Witness.Touch(); return unit; }
                );

            du1Witness.ShouldHaveBeenTouched(1);
            du2Witness.ShouldHaveBeenTouched(0);
        }

        [Property(DisplayName = "Runs T2 when it is T2")]
        public void RunsT2ItersFunc(int a)
        {
            var du1Witness = new Witness();
            var du2Witness = new Witness();

            new Du<string, int>(a)
                .Iter(
                    (string _) => { du1Witness.Touch(); return unit; },
                    (int _) => { du2Witness.Touch(); return unit; }
                );

            du1Witness.ShouldHaveBeenTouched(0);
            du2Witness.ShouldHaveBeenTouched(1);
        }

        [Property(DisplayName = "Runs T1 when it is T1")]
        public void RunsT1OnFunc(int a)
        {
            var witness = new Witness();

            new Du<int, string>(a)
                .Iter((int _) => { witness.Touch(); return unit; });

            witness.ShouldHaveBeenTouched(1);
        }

        [Property(DisplayName = "Does not run for T1 when it's T2")]
        public void RunsT2OnFunc(int a)
        {
            var witness = new Witness();

            new Du<string, int>(a)
                .Iter((int _) => { witness.Touch(); return unit; });

            witness.ShouldHaveBeenTouched(1);
        }

        [Property(DisplayName = "Does not run for T1 when it's T2")]
        public void Skips1When2OnFunc(int a)
        {
            var witness = new Witness();

            new Du<string, int>(a)
                .Iter((string _) => { witness.Touch(); return unit; });

            witness.ShouldHaveBeenTouched(0);
        }

        [Property(DisplayName = "Does not run for T2 when it's T1")]
        public void Skips2When1OnFunc(int a)
        {
            var witness = new Witness();

            new Du<int, string>(a)
                .Iter((string _) => { witness.Touch(); return unit; });

            witness.ShouldHaveBeenTouched(0);
        }
    }
}
