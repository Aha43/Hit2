namespace Hit2.Exceptions
{
    public sealed class TestNotFoundException : Exception
    {
        public TestNotFoundException(string name) : base(name) { }
    }
}
