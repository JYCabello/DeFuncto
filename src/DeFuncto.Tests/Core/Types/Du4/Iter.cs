using DeFuncto.Assertions;
using DeFuncto.Tests.Models;
using FsCheck.Xunit;

namespace DeFuncto.Tests.Core.Types.Du4;

public class Iter
{
    [Property(DisplayName = "Runs T1 when it is T1")]
    public void RunsT1(int a)
    {
        var touchedWitness = new Witness();
        var notTouchedWitness = new Witness();

        new Du4<int, Model1, Model2, Model3>(a)
            .Iter(
                _ => { touchedWitness.Touch(); },
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

        new Du4<Model1, int, Model2, Model3>(a)
            .Iter(
                _ => { notTouchedWitness.Touch(); },
                _ => { touchedWitness.Touch(); },
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

        new Du4<Model1, Model2, int, Model3>(a)
            .Iter(
                _ => { notTouchedWitness.Touch(); },
                _ => { notTouchedWitness.Touch(); },
                _ => { touchedWitness.Touch(); },
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

        new Du4<Model1, Model2, Model3, int>(a)
            .Iter(
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

        new Du4<int, Model1, Model2, Model3>(a)
            .Iter(
                _ => touchedWitness.Touch(),
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

        new Du4<Model1, int, Model2, Model3>(a)
            .Iter(
                _ => notTouchedWitness.Touch(),
                _ => touchedWitness.Touch(),
                _ => notTouchedWitness.Touch(),
                _ => notTouchedWitness.Touch()
            );

        touchedWitness.ShouldHaveBeenTouched(1);
        notTouchedWitness.ShouldHaveBeenTouched(0);
    }

    [Property(DisplayName = "Runs T3 when it is T3")]
    public void RunsT3IterFuncs(int a)
    {
        var touchedWitness = new Witness();
        var notTouchedWitness = new Witness();

        new Du4<Model1, Model2, int, Model3>(a)
            .Iter(
                _ => notTouchedWitness.Touch(),
                _ => notTouchedWitness.Touch(),
                _ => touchedWitness.Touch(),
                _ => notTouchedWitness.Touch()
            );

        touchedWitness.ShouldHaveBeenTouched(1);
        notTouchedWitness.ShouldHaveBeenTouched(0);
    }

    [Property(DisplayName = "Runs T4 when it is T4")]
    public void RunsT4IterFuncs(int a)
    {
        var touchedWitness = new Witness();
        var notTouchedWitness = new Witness();

        new Du4<Model1, Model2, Model3, int>(a)
            .Iter(
                _ => notTouchedWitness.Touch(),
                _ => notTouchedWitness.Touch(),
                _ => notTouchedWitness.Touch(),
                _ => touchedWitness.Touch()
            );

        touchedWitness.ShouldHaveBeenTouched(1);
        notTouchedWitness.ShouldHaveBeenTouched(0);
    }

    [Property(DisplayName = "Runs T1 when it is T1")]
    public void RunsT1OnFunc(int a)
    {
        var touchedWitness = new Witness();
        var notTouchedWitness = new Witness();

        var du = new Du4<int, Model1, Model2, Model3>(a);

        du.Iter((int _) => touchedWitness.Touch());

        du.Iter((Model1 _) => notTouchedWitness.Touch());
        du.Iter((Model2 _) => notTouchedWitness.Touch());
        du.Iter((Model3 _) => notTouchedWitness.Touch());

        touchedWitness.ShouldHaveBeenTouched(1);
        notTouchedWitness.ShouldHaveBeenTouched(0);
    }

    [Property(DisplayName = "Runs T2 when it is T2")]
    public void RunsT2OnFunc(int a)
    {
        var touchedWitness = new Witness();
        var notTouchedWitness = new Witness();

        var du = new Du4<Model1, int, Model2, Model3>(a);

        du.Iter((int _) => touchedWitness.Touch());

        du.Iter((Model1 _) => notTouchedWitness.Touch());
        du.Iter((Model2 _) => notTouchedWitness.Touch());
        du.Iter((Model3 _) => notTouchedWitness.Touch());

        touchedWitness.ShouldHaveBeenTouched(1);
        notTouchedWitness.ShouldHaveBeenTouched(0);
    }

    [Property(DisplayName = "Runs T3 when it is T3")]
    public void RunsT3OnFunc(int a)
    {
        var touchedWitness = new Witness();
        var notTouchedWitness = new Witness();

        var du = new Du4<Model1, Model2, int, Model3>(a);

        du.Iter((int _) => touchedWitness.Touch());

        du.Iter((Model1 _) => notTouchedWitness.Touch());
        du.Iter((Model2 _) => notTouchedWitness.Touch());
        du.Iter((Model3 _) => notTouchedWitness.Touch());

        touchedWitness.ShouldHaveBeenTouched(1);
        notTouchedWitness.ShouldHaveBeenTouched(0);
    }

    [Property(DisplayName = "Runs T4 when it is T4")]
    public void RunsT4OnFunc(int a)
    {
        var touchedWitness = new Witness();
        var notTouchedWitness = new Witness();

        var du = new Du4<Model1, Model2, Model3, int>(a);

        du.Iter((int _) => touchedWitness.Touch());

        du.Iter((Model1 _) => notTouchedWitness.Touch());
        du.Iter((Model2 _) => notTouchedWitness.Touch());
        du.Iter((Model3 _) => notTouchedWitness.Touch());

        touchedWitness.ShouldHaveBeenTouched(1);
        notTouchedWitness.ShouldHaveBeenTouched(0);
    }
}
