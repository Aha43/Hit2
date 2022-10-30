namespace Hit2.Exceptions
{
    public sealed class TestDataValueNotOfTypeException<T> : Exception where T : class
    {
        internal TestDataValueNotOfTypeException(string name) : base($"Test data '{name}' not of type '{typeof(T)}'") { }
    }
}
