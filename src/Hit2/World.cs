namespace Hit2
{
    public sealed class World
    {
        private readonly Dictionary<string, object> _attributes = new();

        public World SetAttribute(string name, object? value)
        {
            _attributes.Add(name, value);
            return this;
        }

        public T? GetAttribute<T>(string name) where T : class
        {
            if (_attributes.TryGetValue(name, out var value))
            {
                if (value == null)
                {
                    return null;
                }

                if (value is not T retVal)
                {
                    throw new ArgumentException($"Attribute '{name}' value not of required type");
                }

                return retVal;
            }

            return null;
        }

        public T GetRequiredAttribute<T>(string name) where T : class
        {
            if (_attributes.TryGetValue(name, out var value))
            {
                if (value == null)
                {
                    throw new ArgumentException($"Argument '{name}' has null value");
                }

                if (value is not T retVal)
                {
                    throw new ArgumentException($"Attribute '{name}' value not of required type");
                }

                return retVal;
            }

            throw new ArgumentException($"Attribute '{name}' not found");
        }

    }

}
