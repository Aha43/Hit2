using Hit2.Exceptions;

namespace Hit2
{
    public class TestData
    {
        private readonly Dictionary<string, object> _data = new();

        internal bool Empty => !_data.Any();

        internal void Set(string name, object value) => _data[name] = value;

        public T Get<T>(string name) where T : class
        {
            if (_data.TryGetValue(name, out var val))
            {
                if (val is not T retVal)
                {
                    throw new TestDataValueNotOfTypeException<T>(name);
                }

                return retVal;
            }

            throw new TestDataNotFoundException(name);
        }

    }
}
