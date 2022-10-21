namespace Hit2
{
    public sealed class World
    {
        private readonly Dictionary<string, object> _attributes = new();

        public World SetAttribute(string name, object value)
        {
            _attributes.Add(name, value);
            return this;
        }

        public T? GetAttribute<T>(string name) where T : class
        {
            if (_attributes.TryGetValue(name, out var value))
            {
                return value as T;
            }

            return null;
        }
        
    }

}
