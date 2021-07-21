namespace DeFuncto.Types
{
    internal abstract class TypedValue<T>
    {
        protected TypedValue(T value) =>
            Value = value;

        public T Value { get; }
    }
}
