namespace Hit2.Exceptions
{
    public sealed class TestNodeNotFoundException : Exception
    {
        public TestNodeNotFoundException(string name) : base(name) { }
    }
}
