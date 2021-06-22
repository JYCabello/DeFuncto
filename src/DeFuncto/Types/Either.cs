namespace DeFuncto
{
    public readonly struct Either<TLeft, TRight>
    {
        internal readonly TLeft? Alternative;
        internal readonly TRight? Value;
        public readonly bool IsRight;
        public bool IsLeft => !IsRight;

        private Either(TLeft left)
        {
            Alternative = left;
            Value = default;
            IsRight = false;
        }

        private Either(TRight right)
        {
            Alternative = default;
            Value = right;
            IsRight = true;
        }

        public static Either<TLeft, TRight> Right(TRight right) => new(right);
        public static Either<TLeft, TRight> Left(TLeft left) => new(left);
    }
}
