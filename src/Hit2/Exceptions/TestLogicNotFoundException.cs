namespace Hit2.Exceptions
{
    public sealed class TestLogicNotFoundException : Exception
    {
        internal TestLogicNotFoundException(string test, string logic) : base($"Test logic named '{logic}' not found for test named '{test}'") { }
    }
}
