using Hit2.Exceptions;

namespace Hit2
{
    public sealed class Claims
    {
        private readonly Dictionary<string, object> _claims = new();

        public bool Exist(string name) => _claims.ContainsKey(name);

        public Claims Add(string name, object value)
        {
            if (_claims.ContainsKey(name))
            {
                throw new ClaimExistException(name);
            }

            _claims[name] = value;
            return this;
        }

        public Claims Remove(string name)
        {
            if (_claims.ContainsKey(name))
            {
                _claims.Remove(name);
                return this;
            }

            throw new ClaimDoesNotExistException(name);
        }

        public T Get<T>(string name) where T : class
        {
            if (_claims.TryGetValue(name, out object? value))
            {
                if (value is T retVal)
                {
                    return retVal;
                }

                throw new ClaimValueNotOfTypeException<T>(name);
            }

            throw new ClaimDoesNotExistException(name);
        }

        public Claims ChangeName(string oldName, string newName)
        {
            if (_claims.ContainsKey(oldName))
            {
                if (_claims.ContainsKey(newName))
                {
                    throw new ClaimExistException(newName);
                }

                var value = _claims[oldName];
                _claims.Remove(oldName);
                _claims.Add(newName, value);
                return this;
            }

            throw new ClaimDoesNotExistException(oldName);
        }

        public Claims ChangeValue(string name, object newValue)
        {
            if (_claims.ContainsKey(name))
            {
                _claims[name] = newValue;
                return this;
            }

            throw new ClaimDoesNotExistException(name);
        }

    }

}
