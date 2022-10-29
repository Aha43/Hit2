namespace Hit2.Exceptions
{
    public sealed class ClaimExistException : Exception
    {
        internal ClaimExistException(string name) : base($"Claim '{name}' exist") { }
    }
}
