namespace DeFuncto
{
    public readonly struct Unit
    {
        public static Unit Default { get; } = new();
        public override bool Equals(object obj) => obj is Unit;
        public bool Equals(Unit other) => true;
        public override int GetHashCode() => 0;
    }
}
