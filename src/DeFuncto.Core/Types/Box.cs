namespace DeFuncto.Types
{
    internal class Box<T> : TypedValue<T>
    {
        public Box(T value) : base(value) { }
    }
}
