using DeFuncto.Assertions;
using FsCheck.Xunit;

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
                    _ => { du1Witness.Touch(); },
                    _ => { du2Witness.Touch(); }
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
                    _ => { du1Witness.Touch(); },
                    _ => { du2Witness.Touch(); }
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
                    _ => du1Witness.Touch(),
                    _ => du2Witness.Touch()
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
                    _ => du1Witness.Touch(),
                    _ => du2Witness.Touch()
                );

            du1Witness.ShouldHaveBeenTouched(0);
            du2Witness.ShouldHaveBeenTouched(1);
        }

        [Property(DisplayName = "Runs T1 when it is T1")]
        public void RunsT1OnFunc(int a)
        {
            var witness = new Witness();

            new Du<int, string>(a)
                .Iter((int _) => witness.Touch());

            witness.ShouldHaveBeenTouched(1);
        }

        [Property(DisplayName = "Does not run for T1 when it's T2")]
        public void RunsT2OnFunc(int a)
        {
            var witness = new Witness();

            new Du<string, int>(a)
                .Iter((int _) => witness.Touch());

            witness.ShouldHaveBeenTouched(1);
        }

        [Property(DisplayName = "Does not run for T1 when it's T2")]
        public void Skips1When2OnFunc(int a)
        {
            var witness = new Witness();

            new Du<string, int>(a)
                .Iter((string _) => witness.Touch());

            witness.ShouldHaveBeenTouched(0);
        }

        [Property(DisplayName = "Does not run for T2 when it's T1")]
        public void Skips2When1OnFunc(int a)
        {
            var witness = new Witness();

            new Du<int, string>(a)
                .Iter((string _) => witness.Touch());

            witness.ShouldHaveBeenTouched(0);
        }
    }
}
