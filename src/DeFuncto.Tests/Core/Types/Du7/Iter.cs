using DeFuncto.Assertions;
using DeFuncto.Tests.Models;
using FsCheck.Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.Du7;

public class Iter
{
    [Property(DisplayName = "Runs T1 when it is T1")]
    public void RunsT1(int a)
    {
        var touchedWitness = new Witness();
        var notTouchedWitness = new Witness();

        new Du7<int, Model1, Model2, Model3, Model4, Model5, Model6>(a)
            .Iter(
                _ => { touchedWitness.Touch(); },
                _ => { notTouchedWitness.Touch(); },
                _ => { notTouchedWitness.Touch(); },
                _ => { notTouchedWitness.Touch(); },
                _ => { notTouchedWitness.Touch(); },
                _ => { notTouchedWitness.Touch(); },
                _ => { notTouchedWitness.Touch(); }
            );

        touchedWitness.ShouldHaveBeenTouched(1);
        notTouchedWitness.ShouldHaveBeenTouched(0);
    }

    [Property(DisplayName = "Runs T2 when it is T2")]
    public void RunsT2(int a)
    {
        var touchedWitness = new Witness();
        var notTouchedWitness = new Witness();

        new Du7<Model1, int, Model2, Model3, Model4, Model5, Model6>(a)
            .Iter(
                _ => { notTouchedWitness.Touch(); },
                _ => { touchedWitness.Touch(); },
                _ => { notTouchedWitness.Touch(); },
                _ => { notTouchedWitness.Touch(); },
                _ => { notTouchedWitness.Touch(); },
                _ => { notTouchedWitness.Touch(); },
                _ => { notTouchedWitness.Touch(); }
            );

        touchedWitness.ShouldHaveBeenTouched(1);
        notTouchedWitness.ShouldHaveBeenTouched(0);
    }

    [Property(DisplayName = "Runs T3 when it is T3")]
    public void RunsT3(int a)
    {
        var touchedWitness = new Witness();
        var notTouchedWitness = new Witness();

        new Du7<Model1, Model2, int, Model3, Model4, Model5, Model6>(a)
            .Iter(
                _ => { notTouchedWitness.Touch(); },
                _ => { notTouchedWitness.Touch(); },
                _ => { touchedWitness.Touch(); },
                _ => { notTouchedWitness.Touch(); },
                _ => { notTouchedWitness.Touch(); },
                _ => { notTouchedWitness.Touch(); },
                _ => { notTouchedWitness.Touch(); }
            );

        touchedWitness.ShouldHaveBeenTouched(1);
        notTouchedWitness.ShouldHaveBeenTouched(0);
    }

    [Property(DisplayName = "Runs T4 when it is T4")]
    public void RunsT4(int a)
    {
        var touchedWitness = new Witness();
        var notTouchedWitness = new Witness();

        new Du7<Model1, Model2, Model3, int, Model4, Model5, Model6>(a)
            .Iter(
                _ => { notTouchedWitness.Touch(); },
                _ => { notTouchedWitness.Touch(); },
                _ => { notTouchedWitness.Touch(); },
                _ => { touchedWitness.Touch(); },
                _ => { notTouchedWitness.Touch(); },
                _ => { notTouchedWitness.Touch(); },
                _ => { notTouchedWitness.Touch(); }
            );

        touchedWitness.ShouldHaveBeenTouched(1);
        notTouchedWitness.ShouldHaveBeenTouched(0);
    }

    [Property(DisplayName = "Runs T5 when it is T5")]
    public void RunsT5(int a)
    {
        var touchedWitness = new Witness();
        var notTouchedWitness = new Witness();

        new Du7<Model1, Model2, Model3, Model4, int, Model5, Model6>(a)
            .Iter(
                _ => { notTouchedWitness.Touch(); },
                _ => { notTouchedWitness.Touch(); },
                _ => { notTouchedWitness.Touch(); },
                _ => { notTouchedWitness.Touch(); },
                _ => { touchedWitness.Touch(); },
                _ => { notTouchedWitness.Touch(); },
                _ => { notTouchedWitness.Touch(); }
            );

        touchedWitness.ShouldHaveBeenTouched(1);
        notTouchedWitness.ShouldHaveBeenTouched(0);
    }

    [Property(DisplayName = "Runs T6 when it is T6")]
    public void RunsT6(int a)
    {
        var touchedWitness = new Witness();
        var notTouchedWitness = new Witness();

        new Du7<Model1, Model2, Model3, Model4, Model5, int, Model6>(a)
            .Iter(
                _ => { notTouchedWitness.Touch(); },
                _ => { notTouchedWitness.Touch(); },
                _ => { notTouchedWitness.Touch(); },
                _ => { notTouchedWitness.Touch(); },
                _ => { notTouchedWitness.Touch(); },
                _ => { touchedWitness.Touch(); },
                _ => { notTouchedWitness.Touch(); }
            );

        touchedWitness.ShouldHaveBeenTouched(1);
        notTouchedWitness.ShouldHaveBeenTouched(0);
    }

    [Property(DisplayName = "Runs T7 when it is T7")]
    public void RunsT7(int a)
    {
        var touchedWitness = new Witness();
        var notTouchedWitness = new Witness();

        new Du7<Model1, Model2, Model3, Model4, Model5, Model6, int>(a)
            .Iter(
                _ => { notTouchedWitness.Touch(); },
                _ => { notTouchedWitness.Touch(); },
                _ => { notTouchedWitness.Touch(); },
                _ => { notTouchedWitness.Touch(); },
                _ => { notTouchedWitness.Touch(); },
                _ => { notTouchedWitness.Touch(); },
                _ => { touchedWitness.Touch(); }
            );

        touchedWitness.ShouldHaveBeenTouched(1);
        notTouchedWitness.ShouldHaveBeenTouched(0);
    }

    [Property(DisplayName = "Runs T1 when it is T1")]
    public void RunsT1IterFuncs(int a)
    {
        var touchedWitness = new Witness();
        var notTouchedWitness = new Witness();

        new Du7<int, Model1, Model2, Model3, Model4, Model5, Model6>(a)
            .Iter(
                _ => touchedWitness.Touch(),
                _ => notTouchedWitness.Touch(),
                _ => notTouchedWitness.Touch(),
                _ => notTouchedWitness.Touch(),
                _ => notTouchedWitness.Touch(),
                _ => notTouchedWitness.Touch(),
                _ => notTouchedWitness.Touch()
            );

        touchedWitness.ShouldHaveBeenTouched(1);
        notTouchedWitness.ShouldHaveBeenTouched(0);
    }

    [Property(DisplayName = "Runs T2 when it is T2")]
    public void RunsT2IterFuncs(int a)
    {
        var touchedWitness = new Witness();
        var notTouchedWitness = new Witness();

        new Du7<Model1, int, Model2, Model3, Model4, Model5, Model6>(a)
            .Iter(
                _ =>
                {
                    notTouchedWitness.Touch();
                    return unit;
                },
                _ =>
                {
                    touchedWitness.Touch();
                    return unit;
                },
                _ =>
                {
                    notTouchedWitness.Touch();
                    return unit;
                },
                _ =>
                {
                    notTouchedWitness.Touch();
                    return unit;
                },
                _ =>
                {
                    notTouchedWitness.Touch();
                    return unit;
                },
                _ =>
                {
                    notTouchedWitness.Touch();
                    return unit;
                },
                _ =>
                {
                    notTouchedWitness.Touch();
                    return unit;
                }
            );

        touchedWitness.ShouldHaveBeenTouched(1);
        notTouchedWitness.ShouldHaveBeenTouched(0);
    }

    [Property(DisplayName = "Runs T3 when it is T3")]
    public void RunsT3IterFuncs(int a)
    {
        var touchedWitness = new Witness();
        var notTouchedWitness = new Witness();

        new Du7<Model1, Model2, int, Model3, Model4, Model5, Model6>(a)
            .Iter(
                _ =>
                {
                    notTouchedWitness.Touch();
                    return unit;
                },
                _ =>
                {
                    notTouchedWitness.Touch();
                    return unit;
                },
                _ =>
                {
                    touchedWitness.Touch();
                    return unit;
                },
                _ =>
                {
                    notTouchedWitness.Touch();
                    return unit;
                },
                _ =>
                {
                    notTouchedWitness.Touch();
                    return unit;
                },
                _ =>
                {
                    notTouchedWitness.Touch();
                    return unit;
                },
                _ =>
                {
                    notTouchedWitness.Touch();
                    return unit;
                }
            );

        touchedWitness.ShouldHaveBeenTouched(1);
        notTouchedWitness.ShouldHaveBeenTouched(0);
    }

    [Property(DisplayName = "Runs T4 when it is T4")]
    public void RunsT4IterFuncs(int a)
    {
        var touchedWitness = new Witness();
        var notTouchedWitness = new Witness();

        new Du7<Model1, Model2, Model3, int, Model4, Model5, Model6>(a)
            .Iter(
                _ =>
                {
                    notTouchedWitness.Touch();
                    return unit;
                },
                _ =>
                {
                    notTouchedWitness.Touch();
                    return unit;
                },
                _ =>
                {
                    notTouchedWitness.Touch();
                    return unit;
                },
                _ =>
                {
                    touchedWitness.Touch();
                    return unit;
                },
                _ =>
                {
                    notTouchedWitness.Touch();
                    return unit;
                },
                _ =>
                {
                    notTouchedWitness.Touch();
                    return unit;
                },
                _ =>
                {
                    notTouchedWitness.Touch();
                    return unit;
                }
            );

        touchedWitness.ShouldHaveBeenTouched(1);
        notTouchedWitness.ShouldHaveBeenTouched(0);
    }

    [Property(DisplayName = "Runs T5 when it is T5")]
    public void RunsT5IterFuncs(int a)
    {
        var touchedWitness = new Witness();
        var notTouchedWitness = new Witness();

        new Du7<Model1, Model2, Model3, Model4, int, Model5, Model6>(a)
            .Iter(
                _ =>
                {
                    notTouchedWitness.Touch();
                    return unit;
                },
                _ =>
                {
                    notTouchedWitness.Touch();
                    return unit;
                },
                _ =>
                {
                    notTouchedWitness.Touch();
                    return unit;
                },
                _ =>
                {
                    notTouchedWitness.Touch();
                    return unit;
                },
                _ =>
                {
                    touchedWitness.Touch();
                    return unit;
                },
                _ =>
                {
                    notTouchedWitness.Touch();
                    return unit;
                },
                _ =>
                {
                    notTouchedWitness.Touch();
                    return unit;
                }
            );

        touchedWitness.ShouldHaveBeenTouched(1);
        notTouchedWitness.ShouldHaveBeenTouched(0);
    }

    [Property(DisplayName = "Runs T6 when it is T6")]
    public void RunsT6IterFuncs(int a)
    {
        var touchedWitness = new Witness();
        var notTouchedWitness = new Witness();

        new Du7<Model1, Model2, Model3, Model4, Model5, int, Model6>(a)
            .Iter(
                _ =>
                {
                    notTouchedWitness.Touch();
                    return unit;
                },
                _ =>
                {
                    notTouchedWitness.Touch();
                    return unit;
                },
                _ =>
                {
                    notTouchedWitness.Touch();
                    return unit;
                },
                _ =>
                {
                    notTouchedWitness.Touch();
                    return unit;
                },
                _ =>
                {
                    notTouchedWitness.Touch();
                    return unit;
                },
                _ =>
                {
                    touchedWitness.Touch();
                    return unit;
                },
                _ =>
                {
                    notTouchedWitness.Touch();
                    return unit;
                }
            );

        touchedWitness.ShouldHaveBeenTouched(1);
        notTouchedWitness.ShouldHaveBeenTouched(0);
    }

    [Property(DisplayName = "Runs T7 when it is T7")]
    public void RunsT7IterFuncs(int a)
    {
        var touchedWitness = new Witness();
        var notTouchedWitness = new Witness();

        new Du7<Model1, Model2, Model3, Model4, Model5, Model6, int>(a)
            .Iter(
                _ =>
                {
                    notTouchedWitness.Touch();
                    return unit;
                },
                _ =>
                {
                    notTouchedWitness.Touch();
                    return unit;
                },
                _ =>
                {
                    notTouchedWitness.Touch();
                    return unit;
                },
                _ =>
                {
                    notTouchedWitness.Touch();
                    return unit;
                },
                _ =>
                {
                    notTouchedWitness.Touch();
                    return unit;
                },
                _ =>
                {
                    notTouchedWitness.Touch();
                    return unit;
                },
                _ =>
                {
                    touchedWitness.Touch();
                    return unit;
                }
            );

        touchedWitness.ShouldHaveBeenTouched(1);
        notTouchedWitness.ShouldHaveBeenTouched(0);
    }

    [Property(DisplayName = "Runs T1 when it is T1")]
    public void RunsT1OnFunc(int a)
    {
        var touchedWitness = new Witness();
        var notTouchedWitness = new Witness();

        var du = new Du7<int, Model1, Model2, Model3, Model4, Model5, Model6>(a);

        du.Iter((int _) =>
        {
            touchedWitness.Touch();
            return unit;
        });

        du.Iter((Model1 _) =>
        {
            notTouchedWitness.Touch();
            return unit;
        });
        du.Iter((Model2 _) =>
        {
            notTouchedWitness.Touch();
            return unit;
        });
        du.Iter((Model3 _) =>
        {
            notTouchedWitness.Touch();
            return unit;
        });
        du.Iter((Model4 _) =>
        {
            notTouchedWitness.Touch();
            return unit;
        });
        du.Iter((Model5 _) =>
        {
            notTouchedWitness.Touch();
            return unit;
        });
        du.Iter((Model6 _) =>
        {
            notTouchedWitness.Touch();
            return unit;
        });

        touchedWitness.ShouldHaveBeenTouched(1);
        notTouchedWitness.ShouldHaveBeenTouched(0);
    }

    [Property(DisplayName = "Runs T2 when it is T2")]
    public void RunsT2OnFunc(int a)
    {
        var touchedWitness = new Witness();
        var notTouchedWitness = new Witness();

        var du = new Du7<Model1, int, Model2, Model3, Model4, Model5, Model6>(a);

        du.Iter((int _) =>
        {
            touchedWitness.Touch();
            return unit;
        });

        du.Iter((Model1 _) =>
        {
            notTouchedWitness.Touch();
            return unit;
        });
        du.Iter((Model2 _) =>
        {
            notTouchedWitness.Touch();
            return unit;
        });
        du.Iter((Model3 _) =>
        {
            notTouchedWitness.Touch();
            return unit;
        });
        du.Iter((Model4 _) =>
        {
            notTouchedWitness.Touch();
            return unit;
        });
        du.Iter((Model5 _) =>
        {
            notTouchedWitness.Touch();
            return unit;
        });
        du.Iter((Model6 _) =>
        {
            notTouchedWitness.Touch();
            return unit;
        });

        touchedWitness.ShouldHaveBeenTouched(1);
        notTouchedWitness.ShouldHaveBeenTouched(0);
    }

    [Property(DisplayName = "Runs T3 when it is T3")]
    public void RunsT3OnFunc(int a)
    {
        var touchedWitness = new Witness();
        var notTouchedWitness = new Witness();

        var du = new Du7<Model1, Model2, int, Model3, Model4, Model5, Model6>(a);

        du.Iter((int _) =>
        {
            touchedWitness.Touch();
            return unit;
        });

        du.Iter((Model1 _) =>
        {
            notTouchedWitness.Touch();
            return unit;
        });
        du.Iter((Model2 _) =>
        {
            notTouchedWitness.Touch();
            return unit;
        });
        du.Iter((Model3 _) =>
        {
            notTouchedWitness.Touch();
            return unit;
        });
        du.Iter((Model4 _) =>
        {
            notTouchedWitness.Touch();
            return unit;
        });
        du.Iter((Model5 _) =>
        {
            notTouchedWitness.Touch();
            return unit;
        });
        du.Iter((Model6 _) =>
        {
            notTouchedWitness.Touch();
            return unit;
        });

        touchedWitness.ShouldHaveBeenTouched(1);
        notTouchedWitness.ShouldHaveBeenTouched(0);
    }

    [Property(DisplayName = "Runs T4 when it is T4")]
    public void RunsT4OnFunc(int a)
    {
        var touchedWitness = new Witness();
        var notTouchedWitness = new Witness();

        var du = new Du7<Model1, Model2, Model3, int, Model4, Model5, Model6>(a);

        du.Iter((int _) =>
        {
            touchedWitness.Touch();
            return unit;
        });

        du.Iter((Model1 _) =>
        {
            notTouchedWitness.Touch();
            return unit;
        });
        du.Iter((Model2 _) =>
        {
            notTouchedWitness.Touch();
            return unit;
        });
        du.Iter((Model3 _) =>
        {
            notTouchedWitness.Touch();
            return unit;
        });
        du.Iter((Model4 _) =>
        {
            notTouchedWitness.Touch();
            return unit;
        });
        du.Iter((Model5 _) =>
        {
            notTouchedWitness.Touch();
            return unit;
        });
        du.Iter((Model6 _) =>
        {
            notTouchedWitness.Touch();
            return unit;
        });

        touchedWitness.ShouldHaveBeenTouched(1);
        notTouchedWitness.ShouldHaveBeenTouched(0);
    }

    [Property(DisplayName = "Runs T5 when it is T5")]
    public void RunsT5OnFunc(int a)
    {
        var touchedWitness = new Witness();
        var notTouchedWitness = new Witness();

        var du = new Du7<Model1, Model2, Model3, Model4, int, Model5, Model6>(a);

        du.Iter((int _) =>
        {
            touchedWitness.Touch();
            return unit;
        });

        du.Iter((Model1 _) =>
        {
            notTouchedWitness.Touch();
            return unit;
        });
        du.Iter((Model2 _) =>
        {
            notTouchedWitness.Touch();
            return unit;
        });
        du.Iter((Model3 _) =>
        {
            notTouchedWitness.Touch();
            return unit;
        });
        du.Iter((Model4 _) =>
        {
            notTouchedWitness.Touch();
            return unit;
        });
        du.Iter((Model5 _) =>
        {
            notTouchedWitness.Touch();
            return unit;
        });
        du.Iter((Model6 _) =>
        {
            notTouchedWitness.Touch();
            return unit;
        });

        touchedWitness.ShouldHaveBeenTouched(1);
        notTouchedWitness.ShouldHaveBeenTouched(0);
    }

    [Property(DisplayName = "Runs T6 when it is T6")]
    public void RunsT6OnFunc(int a)
    {
        var touchedWitness = new Witness();
        var notTouchedWitness = new Witness();

        var du = new Du7<Model1, Model2, Model3, Model4, Model5, int, Model6>(a);

        du.Iter((int _) =>
        {
            touchedWitness.Touch();
            return unit;
        });

        du.Iter((Model1 _) =>
        {
            notTouchedWitness.Touch();
            return unit;
        });
        du.Iter((Model2 _) =>
        {
            notTouchedWitness.Touch();
            return unit;
        });
        du.Iter((Model3 _) =>
        {
            notTouchedWitness.Touch();
            return unit;
        });
        du.Iter((Model4 _) =>
        {
            notTouchedWitness.Touch();
            return unit;
        });
        du.Iter((Model5 _) =>
        {
            notTouchedWitness.Touch();
            return unit;
        });
        du.Iter((Model6 _) =>
        {
            notTouchedWitness.Touch();
            return unit;
        });

        touchedWitness.ShouldHaveBeenTouched(1);
        notTouchedWitness.ShouldHaveBeenTouched(0);
    }

    [Property(DisplayName = "Runs T7 when it is T7")]
    public void RunsT7OnFunc(int a)
    {
        var touchedWitness = new Witness();
        var notTouchedWitness = new Witness();

        var du = new Du7<Model1, Model2, Model3, Model4, Model5, Model6, int>(a);

        du.Iter((int _) =>
        {
            touchedWitness.Touch();
            return unit;
        });

        du.Iter((Model1 _) =>
        {
            notTouchedWitness.Touch();
            return unit;
        });
        du.Iter((Model2 _) =>
        {
            notTouchedWitness.Touch();
            return unit;
        });
        du.Iter((Model3 _) =>
        {
            notTouchedWitness.Touch();
            return unit;
        });
        du.Iter((Model4 _) =>
        {
            notTouchedWitness.Touch();
            return unit;
        });
        du.Iter((Model5 _) =>
        {
            notTouchedWitness.Touch();
            return unit;
        });
        du.Iter((Model6 _) =>
        {
            notTouchedWitness.Touch();
            return unit;
        });

        touchedWitness.ShouldHaveBeenTouched(1);
        notTouchedWitness.ShouldHaveBeenTouched(0);
    }
}
