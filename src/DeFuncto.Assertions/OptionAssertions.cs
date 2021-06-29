using System;
using static DeFuncto.Prelude;

namespace DeFuncto.Assertions
{
    public static class OptionAssertions
    {
        public static Option<T> ShouldBeSome<T>(this Option<T> option)
        {
            if (option.IsNone)
                throw new AssertionFailed("Option should have been in the 'Some' state.");
            return option;
        }

        public static Option<T> ShouldBeSome<T>(this Option<T> option, Func<T, Unit> assertion) =>
            option.ShouldBeSome().Map(t =>
            {
                assertion(t);
                return t;
            });

        public static Option<T> ShouldBeSome<T>(this Option<T> option, T expected) =>
            option.ShouldBeSome(t =>
            {
                if (!expected.Equals(t))
                    throw new AssertionFailed($"Option should have value {expected} but it was {t}.");
                return unit;
            });


        public static Option<T> ShouldBeNone<T>(this Option<T> option)
        {
            if (option.IsSome)
                throw new AssertionFailed("Option should have been in the 'None' state.");
            return option;
        }
    }
}
