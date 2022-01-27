using DeFuncto.Assertions;
using DeFuncto.Tests.Models;
using FsCheck;
using FsCheck.Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.Du6
{
    public class Iter
    {
        [Property(DisplayName = "Runs T1 when it is T1")]
        public void RunsT1(int a)
        {
            var touchedWitness = new Witness();
            var notTouchedWitness = new Witness();

            new Du6<int, Model1, Model2, Model3, Model4, Model5>(a)
                .Iter(
                    (int _) => { touchedWitness.Touch(); },
                    (Model1 _) => { notTouchedWitness.Touch(); },
                    (Model2 _) => { notTouchedWitness.Touch(); },
                    (Model3 _) => { notTouchedWitness.Touch(); },
                    (Model4 _) => { notTouchedWitness.Touch(); },
                    (Model5 _) => { notTouchedWitness.Touch(); }
                );

            touchedWitness.ShouldHaveBeenTouched(1);
            notTouchedWitness.ShouldHaveBeenTouched(0);
        }

        [Property(DisplayName = "Runs T2 when it is T2")]
        public void RunsT2(int a)
        {
            var touchedWitness = new Witness();
            var notTouchedWitness = new Witness();

            new Du6<Model1, int, Model2, Model3, Model4, Model5>(a)
                .Iter(
                    (Model1 _) => { notTouchedWitness.Touch(); },
                    (int _) => { touchedWitness.Touch(); },
                    (Model2 _) => { notTouchedWitness.Touch(); },
                    (Model3 _) => { notTouchedWitness.Touch(); },
                    (Model4 _) => { notTouchedWitness.Touch(); },
                    (Model5 _) => { notTouchedWitness.Touch(); }
                );

            touchedWitness.ShouldHaveBeenTouched(1);
            notTouchedWitness.ShouldHaveBeenTouched(0);
        }

        [Property(DisplayName = "Runs T3 when it is T3")]
        public void RunsT3(int a)
        {
            var touchedWitness = new Witness();
            var notTouchedWitness = new Witness();

            new Du6<Model1, Model2, int, Model3, Model4, Model5>(a)
                .Iter(
                    (Model1 _) => { notTouchedWitness.Touch(); },
                    (Model2 _) => { notTouchedWitness.Touch(); },
                    (int _) => { touchedWitness.Touch(); },
                    (Model3 _) => { notTouchedWitness.Touch(); },
                    (Model4 _) => { notTouchedWitness.Touch(); },
                    (Model5 _) => { notTouchedWitness.Touch(); }
                );

            touchedWitness.ShouldHaveBeenTouched(1);
            notTouchedWitness.ShouldHaveBeenTouched(0);
        }

        [Property(DisplayName = "Runs T4 when it is T4")]
        public void RunsT4(int a)
        {
            var touchedWitness = new Witness();
            var notTouchedWitness = new Witness();

            new Du6<Model1, Model2, Model3, int, Model4, Model5>(a)
                .Iter(
                    (Model1 _) => { notTouchedWitness.Touch(); },
                    (Model2 _) => { notTouchedWitness.Touch(); },
                    (Model3 _) => { notTouchedWitness.Touch(); },
                    (int _) => { touchedWitness.Touch(); },
                    (Model4 _) => { notTouchedWitness.Touch(); },
                    (Model5 _) => { notTouchedWitness.Touch(); }
                );

            touchedWitness.ShouldHaveBeenTouched(1);
            notTouchedWitness.ShouldHaveBeenTouched(0);
        }

        [Property(DisplayName = "Runs T5 when it is T5")]
        public void RunsT5(int a)
        {
            var touchedWitness = new Witness();
            var notTouchedWitness = new Witness();

            new Du6<Model1, Model2, Model3, Model4, int, Model5>(a)
                .Iter(
                    (Model1 _) => { notTouchedWitness.Touch(); },
                    (Model2 _) => { notTouchedWitness.Touch(); },
                    (Model3 _) => { notTouchedWitness.Touch(); },
                    (Model4 _) => { notTouchedWitness.Touch(); },
                    (int _) => { touchedWitness.Touch(); },
                    (Model5 _) => { notTouchedWitness.Touch(); }
                );

            touchedWitness.ShouldHaveBeenTouched(1);
            notTouchedWitness.ShouldHaveBeenTouched(0);
        }

        [Property(DisplayName = "Runs T6 when it is T6")]
        public void RunsT6(int a)
        {
            var touchedWitness = new Witness();
            var notTouchedWitness = new Witness();

            new Du6<Model1, Model2, Model3, Model4, Model5, int>(a)
                .Iter(
                    (Model1 _) => { notTouchedWitness.Touch(); },
                    (Model2 _) => { notTouchedWitness.Touch(); },
                    (Model3 _) => { notTouchedWitness.Touch(); },
                    (Model4 _) => { notTouchedWitness.Touch(); },
                    (Model5 _) => { notTouchedWitness.Touch(); },
                    (int _) => { touchedWitness.Touch(); }
                );

            touchedWitness.ShouldHaveBeenTouched(1);
            notTouchedWitness.ShouldHaveBeenTouched(0);
        }

        [Property(DisplayName = "Runs T1 when it is T1")]
        public void RunsT1OnAction(int a)
        {
            var touchedWitness = new Witness();
            var notTouchedWitness = new Witness();

            var du = new Du6<int, Model1, Model2, Model3, Model4, Model5>(a);

            du.Iter((int _) => { touchedWitness.Touch(); });

            du.Iter((Model1 _) => { notTouchedWitness.Touch(); });
            du.Iter((Model2 _) => { notTouchedWitness.Touch(); });
            du.Iter((Model3 _) => { notTouchedWitness.Touch(); });
            du.Iter((Model4 _) => { notTouchedWitness.Touch(); });
            du.Iter((Model5 _) => { notTouchedWitness.Touch(); });


            touchedWitness.ShouldHaveBeenTouched(1);
            notTouchedWitness.ShouldHaveBeenTouched(0);
        }

        [Property(DisplayName = "Runs T2 when it is T2")]
        public void RunsT2OnAction(int a)
        {
            var touchedWitness = new Witness();
            var notTouchedWitness = new Witness();

            var du = new Du6<Model1, int, Model2, Model3, Model4, Model5>(a);

            du.Iter((int _) => { touchedWitness.Touch(); });

            du.Iter((Model1 _) => { notTouchedWitness.Touch(); });
            du.Iter((Model2 _) => { notTouchedWitness.Touch(); });
            du.Iter((Model3 _) => { notTouchedWitness.Touch(); });
            du.Iter((Model4 _) => { notTouchedWitness.Touch(); });
            du.Iter((Model5 _) => { notTouchedWitness.Touch(); });


            touchedWitness.ShouldHaveBeenTouched(1);
            notTouchedWitness.ShouldHaveBeenTouched(0);
        }
        [Property(DisplayName = "Runs T3 when it is T3")]
        public void RunsT3OnAction(int a)
        {
            var touchedWitness = new Witness();
            var notTouchedWitness = new Witness();

            var du = new Du6<Model1, Model2, int, Model3, Model4, Model5>(a);

            du.Iter((int _) => { touchedWitness.Touch(); });

            du.Iter((Model1 _) => { notTouchedWitness.Touch(); });
            du.Iter((Model2 _) => { notTouchedWitness.Touch(); });
            du.Iter((Model3 _) => { notTouchedWitness.Touch(); });
            du.Iter((Model4 _) => { notTouchedWitness.Touch(); });
            du.Iter((Model5 _) => { notTouchedWitness.Touch(); });


            touchedWitness.ShouldHaveBeenTouched(1);
            notTouchedWitness.ShouldHaveBeenTouched(0);
        }
        [Property(DisplayName = "Runs T4 when it is T4")]
        public void RunsT4OnAction(int a)
        {
            var touchedWitness = new Witness();
            var notTouchedWitness = new Witness();

            var du = new Du6<Model1, Model2, Model3, int, Model4, Model5>(a);

            du.Iter((int _) => { touchedWitness.Touch(); });

            du.Iter((Model1 _) => { notTouchedWitness.Touch(); });
            du.Iter((Model2 _) => { notTouchedWitness.Touch(); });
            du.Iter((Model3 _) => { notTouchedWitness.Touch(); });
            du.Iter((Model4 _) => { notTouchedWitness.Touch(); });
            du.Iter((Model5 _) => { notTouchedWitness.Touch(); });


            touchedWitness.ShouldHaveBeenTouched(1);
            notTouchedWitness.ShouldHaveBeenTouched(0);
        }
        [Property(DisplayName = "Runs T5 when it is T5")]
        public void RunsT5OnAction(int a)
        {
            var touchedWitness = new Witness();
            var notTouchedWitness = new Witness();

            var du = new Du6<Model1, Model2, Model3, Model4, int, Model5>(a);

            du.Iter((int _) => { touchedWitness.Touch(); });

            du.Iter((Model1 _) => { notTouchedWitness.Touch(); });
            du.Iter((Model2 _) => { notTouchedWitness.Touch(); });
            du.Iter((Model3 _) => { notTouchedWitness.Touch(); });
            du.Iter((Model4 _) => { notTouchedWitness.Touch(); });
            du.Iter((Model5 _) => { notTouchedWitness.Touch(); });


            touchedWitness.ShouldHaveBeenTouched(1);
            notTouchedWitness.ShouldHaveBeenTouched(0);
        }
        [Property(DisplayName = "Runs T6 when it is T6")]
        public void RunsT6OnAction(int a)
        {
            var touchedWitness = new Witness();
            var notTouchedWitness = new Witness();

            var du = new Du6<Model1, Model2, Model3, Model4, Model5, int>(a);

            du.Iter((int _) => { touchedWitness.Touch(); });

            du.Iter((Model1 _) => { notTouchedWitness.Touch(); });
            du.Iter((Model2 _) => { notTouchedWitness.Touch(); });
            du.Iter((Model3 _) => { notTouchedWitness.Touch(); });
            du.Iter((Model4 _) => { notTouchedWitness.Touch(); });
            du.Iter((Model5 _) => { notTouchedWitness.Touch(); });


            touchedWitness.ShouldHaveBeenTouched(1);
            notTouchedWitness.ShouldHaveBeenTouched(0);
        }

        [Property(DisplayName = "Runs T1 when it is T1")]
        public void RunsT1IterFuncs(int a)
        {
            var touchedWitness = new Witness();
            var notTouchedWitness = new Witness();

            new Du6<int, Model1, Model2, Model3, Model4, Model5>(a)
                .Iter(
                    (int _) => { touchedWitness.Touch(); return unit; },
                    (Model1 _) => { notTouchedWitness.Touch(); return unit; },
                    (Model2 _) => { notTouchedWitness.Touch(); return unit; },
                    (Model3 _) => { notTouchedWitness.Touch(); return unit; },
                    (Model4 _) => { notTouchedWitness.Touch(); return unit; },
                    (Model5 _) => { notTouchedWitness.Touch(); return unit; }
                );

            touchedWitness.ShouldHaveBeenTouched(1);
            notTouchedWitness.ShouldHaveBeenTouched(0);
        }

        [Property(DisplayName = "Runs T2 when it is T2")]
        public void RunsT2IterFuncs(int a)
        {
            var touchedWitness = new Witness();
            var notTouchedWitness = new Witness();

            new Du6<Model1, int, Model2, Model3, Model4, Model5>(a)
                .Iter(
                    (Model1 _) => { notTouchedWitness.Touch(); return unit; },
                    (int _) => { touchedWitness.Touch(); return unit; },
                    (Model2 _) => { notTouchedWitness.Touch(); return unit; },
                    (Model3 _) => { notTouchedWitness.Touch(); return unit; },
                    (Model4 _) => { notTouchedWitness.Touch(); return unit; },
                    (Model5 _) => { notTouchedWitness.Touch(); return unit; }
                );

            touchedWitness.ShouldHaveBeenTouched(1);
            notTouchedWitness.ShouldHaveBeenTouched(0);
        }
        [Property(DisplayName = "Runs T3 when it is T3")]
        public void RunsT3IterFuncs(int a)
        {
            var touchedWitness = new Witness();
            var notTouchedWitness = new Witness();

            new Du6<Model1, Model2, int, Model3, Model4, Model5>(a)
                .Iter(
                    (Model1 _) => { notTouchedWitness.Touch(); return unit; },
                    (Model2 _) => { notTouchedWitness.Touch(); return unit; },
                    (int _) => { touchedWitness.Touch(); return unit; },
                    (Model3 _) => { notTouchedWitness.Touch(); return unit; },
                    (Model4 _) => { notTouchedWitness.Touch(); return unit; },
                    (Model5 _) => { notTouchedWitness.Touch(); return unit; }
                );

            touchedWitness.ShouldHaveBeenTouched(1);
            notTouchedWitness.ShouldHaveBeenTouched(0);
        }
        [Property(DisplayName = "Runs T4 when it is T4")]
        public void RunsT4IterFuncs(int a)
        {
            var touchedWitness = new Witness();
            var notTouchedWitness = new Witness();

            new Du6<Model1, Model2, Model3, int, Model4, Model5>(a)
                .Iter(
                    (Model1 _) => { notTouchedWitness.Touch(); return unit; },
                    (Model2 _) => { notTouchedWitness.Touch(); return unit; },
                    (Model3 _) => { notTouchedWitness.Touch(); return unit; },
                    (int _) => { touchedWitness.Touch(); return unit; },
                    (Model4 _) => { notTouchedWitness.Touch(); return unit; },
                    (Model5 _) => { notTouchedWitness.Touch(); return unit; }
                );

            touchedWitness.ShouldHaveBeenTouched(1);
            notTouchedWitness.ShouldHaveBeenTouched(0);
        }
        [Property(DisplayName = "Runs T5 when it is T5")]
        public void RunsT5IterFuncs(int a)
        {
            var touchedWitness = new Witness();
            var notTouchedWitness = new Witness();

            new Du6<Model1, Model2, Model3, Model4, int, Model5>(a)
                .Iter(
                    (Model1 _) => { notTouchedWitness.Touch(); return unit; },
                    (Model2 _) => { notTouchedWitness.Touch(); return unit; },
                    (Model3 _) => { notTouchedWitness.Touch(); return unit; },
                    (Model4 _) => { notTouchedWitness.Touch(); return unit; },
                    (int _) => { touchedWitness.Touch(); return unit; },
                    (Model5 _) => { notTouchedWitness.Touch(); return unit; }
                );

            touchedWitness.ShouldHaveBeenTouched(1);
            notTouchedWitness.ShouldHaveBeenTouched(0);
        }
        [Property(DisplayName = "Runs T6 when it is T6")]
        public void RunsT6IterFuncs(int a)
        {
            var touchedWitness = new Witness();
            var notTouchedWitness = new Witness();

            new Du6<Model1, Model2, Model3, Model4, Model5, int>(a)
                .Iter(
                    (Model1 _) => { notTouchedWitness.Touch(); return unit; },
                    (Model2 _) => { notTouchedWitness.Touch(); return unit; },
                    (Model3 _) => { notTouchedWitness.Touch(); return unit; },
                    (Model4 _) => { notTouchedWitness.Touch(); return unit; },
                    (Model5 _) => { notTouchedWitness.Touch(); return unit; },
                    (int _) => { touchedWitness.Touch(); return unit; }
                );

            touchedWitness.ShouldHaveBeenTouched(1);
            notTouchedWitness.ShouldHaveBeenTouched(0);
        }
    }
}
