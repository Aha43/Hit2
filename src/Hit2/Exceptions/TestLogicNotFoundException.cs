namespace Hit2.Exceptions
{
    public class TestLogicNotFoundException : Exception
    {
        public TestLogicNotFoundException(string test, string logic) : base($"Test logic named '{logic}' not found for test named '{test}'") { }
    }
}
