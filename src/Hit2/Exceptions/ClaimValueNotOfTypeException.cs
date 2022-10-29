namespace Hit2.Exceptions
{
    public sealed class ClaimValueNotOfTypeException<T> : Exception where T : class
    {
        internal ClaimValueNotOfTypeException(string name) : base($"Claim '{name}' not of type '{typeof(T)}'") { }
    }
}
