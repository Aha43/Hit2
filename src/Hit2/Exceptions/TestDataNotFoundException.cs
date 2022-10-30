namespace Hit2.Exceptions
{
    public sealed class TestDataNotFoundException : Exception
    {
        public TestDataNotFoundException(string name) : base(name) { }
    }
}
