using DeFuncto.Assertions;
using DeFuncto.Tests.Models;
using FsCheck;
using FsCheck.Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.Du3
{
    public class Iter
    {
        [Property(DisplayName = "Runs T1 when it is T1")]
        public void RunsT1(int a)
        {
            var touchedWitness = new Witness();
            var notTouchedWitness = new Witness();

            new Du3<int, string, string>(a)
                .Iter(
                    (int _) => { touchedWitness.Touch(); },
                    (string _) => { notTouchedWitness.Touch(); },
                    (string _) => { notTouchedWitness.Touch(); }
                );

            touchedWitness.ShouldHaveBeenTouched(1);
            notTouchedWitness.ShouldHaveBeenTouched(0);
        }

        [Property(DisplayName = "Runs T2 when it is T2")]
        public void RunsT2(int a)
        {
            var touchedWitness = new Witness();
            var notTouchedWitness = new Witness();

            new Du3<string, int, string>(a)
                .Iter(
                    (string _) => { notTouchedWitness.Touch(); },
                    (int _) => { touchedWitness.Touch(); },
                    (string _) => { notTouchedWitness.Touch(); }
                );

            touchedWitness.ShouldHaveBeenTouched(1);
            notTouchedWitness.ShouldHaveBeenTouched(0);
        }

        [Property(DisplayName = "Runs T3 when it is T3")]
        public void RunsT3(int a)
        {
            var touchedWitness = new Witness();
            var notTouchedWitness = new Witness();

            new Du3<string, string, int>(a)
                .Iter(
                    (string _) => { notTouchedWitness.Touch(); },
                    (string _) => { notTouchedWitness.Touch(); },
                    (int _) => { touchedWitness.Touch(); }
                );

            touchedWitness.ShouldHaveBeenTouched(1);
            notTouchedWitness.ShouldHaveBeenTouched(0);
        }

        [Property(DisplayName = "Runs T1 when it is T1")]
        public void RunsT1IterFuncs(int a)
        {
            var touchedWitness = new Witness();
            var notTouchedWitness = new Witness();

            new Du3<int, string, string>(a)
                .Iter(
                    (int _) => { touchedWitness.Touch(); return unit; },
                    (string _) => { notTouchedWitness.Touch(); return unit; },
                    (string _) => { notTouchedWitness.Touch(); return unit; }
                );

            touchedWitness.ShouldHaveBeenTouched(1);
            notTouchedWitness.ShouldHaveBeenTouched(0);
        }

        [Property(DisplayName = "Runs T2 when it is T2")]
        public void RunsT2IterFuncs(int a)
        {
            var touchedWitness = new Witness();
            var notTouchedWitness = new Witness();

            new Du3<string, int, string>(a)
                .Iter(
                    (string _) => { notTouchedWitness.Touch(); return unit; },
                    (int _) => { touchedWitness.Touch(); return unit; },
                    (string _) => { notTouchedWitness.Touch(); return unit; }
                );

            touchedWitness.ShouldHaveBeenTouched(1);
            notTouchedWitness.ShouldHaveBeenTouched(0);
        }

        [Property(DisplayName = "Runs T2 when it is T2")]
        public void RunsT3IterFuncs(int a)
        {
            var touchedWitness = new Witness();
            var notTouchedWitness = new Witness();

            new Du3<string, string, int>(a)
                .Iter(
                    (string _) => { notTouchedWitness.Touch(); return unit; },
                    (string _) => { notTouchedWitness.Touch(); return unit; },
                    (int _) => { touchedWitness.Touch(); return unit; }
                );

            touchedWitness.ShouldHaveBeenTouched(1);
            notTouchedWitness.ShouldHaveBeenTouched(0);
        }

        [Property(DisplayName = "Runs T1 when it is T1")]
        public void RunsT1OnFunc(int a)
        {
            var touchedWitness = new Witness();
            var notTouchedWitness = new Witness();

            var du = new Du3<int, Model1, Model2>(a);

            du.Iter((int _) => { touchedWitness.Touch(); return unit; });

            du.Iter((Model1 _) => { notTouchedWitness.Touch(); return unit; });
            du.Iter((Model2 _) => { notTouchedWitness.Touch(); return unit; });

            touchedWitness.ShouldHaveBeenTouched(1);
            notTouchedWitness.ShouldHaveBeenTouched(0);
        }

        [Property(DisplayName = "Runs T2 when it is T2")]
        public void RunsT2OnFunc(int a)
        {
            var touchedWitness = new Witness();
            var notTouchedWitness = new Witness();

            var du = new Du3<Model1, int,  Model2>(a);

            du.Iter((int _) => { touchedWitness.Touch(); return unit; });

            du.Iter((Model1 _) => { notTouchedWitness.Touch(); return unit; });
            du.Iter((Model2 _) => { notTouchedWitness.Touch(); return unit; });

            touchedWitness.ShouldHaveBeenTouched(1);
            notTouchedWitness.ShouldHaveBeenTouched(0);
        }

        [Property(DisplayName = "Runs T3 when it is T3")]
        public void RunsT3OnFunc(int a)
        {
            var touchedWitness = new Witness();
            var notTouchedWitness = new Witness();

            var du = new Du3<Model1, Model2, int>(a);

            du.Iter((int _) => { touchedWitness.Touch(); return unit; });

            du.Iter((Model1 _) => { notTouchedWitness.Touch(); return unit; });
            du.Iter((Model2 _) => { notTouchedWitness.Touch(); return unit; });

            touchedWitness.ShouldHaveBeenTouched(1);
            notTouchedWitness.ShouldHaveBeenTouched(0);
        }
    }
}
