using System;

namespace DeFuncto
{
    public readonly struct Du<T1, T2>
    {
        internal enum DiscriminationValue
        {
            T1,
            T2
        }

        private readonly T1? t1;
        private readonly T2? t2;
        internal readonly DiscriminationValue Discriminator;


        public Du(T1 t1)
        {
            this.t1 = t1;
            t2 = default;
            Discriminator = DiscriminationValue.T1;
        }

        public Du(T2 t2)
        {
            this.t2 = t2;
            t1 = default;
            Discriminator = DiscriminationValue.T2;
        }

        public T Match<T>(Func<T1, T> f1, Func<T2, T> f2) =>
            Discriminator == DiscriminationValue.T1
                ? f1(t1!)
                : f2(t2!);

        public static implicit operator Du<T1, T2>(T1 t1) => new(t1);
        public static implicit operator Du<T1, T2>(T2 t2) => new(t2);
    }
}
