namespace Hit2.Exceptions
{
    public sealed class ClaimDoesNotExistException : Exception
    {
        internal ClaimDoesNotExistException(string name) : base($"Claim '{name}' does not exist") { }
    }
}
