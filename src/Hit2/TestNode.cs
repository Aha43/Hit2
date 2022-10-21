using System.Text;

namespace Hit2
{
    public sealed class TestNode
    {
        public string Name { get; init; }

        private string? _unitTest;
        public string UnitTest
        {
            get
            {
                return string.IsNullOrWhiteSpace(_unitTest) ? Name : _unitTest;
            }
        }

        public TestNode AsUnitTest(string name)
        {
            if (!IsLeaf)
            {
                throw new ArgumentException("Only leaf can be assigned the path's unit test name");
            }
            if (!string.IsNullOrWhiteSpace(_unitTest))
            {
                throw new ArgumentException("Path allready got unit test name");
            }

            _unitTest = name;
            return this;
        }

        public TestNode? Parent { get; init; }

        private readonly List<TestNode> _children = new();

        private readonly Dictionary<string, object> _params = new();

        internal TestNode(
            string testName, 
            TestNode? parent = null)
        {
            Name = testName;

            if (parent != null)
            {
                if (!string.IsNullOrWhiteSpace(parent._unitTest))
                {
                    throw new ArgumentException($"Can not add to path allready explicit named as unit test: {parent._unitTest}");
                }
            }

            Parent = parent;
        }

        public IEnumerable<TestNode> Children => _children.AsReadOnly();

        public bool IsLeaf => _children.Count == 0;

        public TestNode Do(string testName)
        {
            var retVal = new TestNode(testName, this);
            _children.Add(retVal);
            return retVal;
        }

        public TestNode With(string name, object value)
        {
            if (_params.Any())
            {
                throw new ArgumentException("Use With only for first parameter");
            }

            return SetParam(name, value);
        }

        public TestNode And(string name, object value)
        {
            if (!_params.Any())
            {
                throw new ArgumentException("Use With for first parameter");
            }

            return SetParam(name, value);
        }

        private TestNode SetParam(string name, object value)
        {
            if (_params.ContainsKey(name))
            {
                throw new ArgumentException($"Duplicate parameter {name}");
            }

            _params[name] = value;
            return this;
        }

        public TestNode From(string name)
        {
            var current = this;
            while (current != null)
            {
                if (current.Name.Equals(name))
                {
                    return current;
                }
                current = current.Parent;
            }

            throw new ArgumentException($"ancestor test node named {name} not found");
        }

        public TestNode[] GetPath()
        {
            var stack = new Stack<TestNode>();
            var current = this;
            while (current != null)
            {
                stack.Push(current);
                current = current.Parent;
            }

            return stack.ToArray();
        }

        public override string ToString()
        {
            var sb = new StringBuilder(Name);
            if (_params.Any())
            {
                sb.Append(" with ");
                var first = true;
                foreach (var e in _params)
                {
                    if (!first)
                    {
                        sb.Append(" and ");
                    }
                    first = false;

                    sb.Append($"{e.Key} is {e.Value}");
                }
            }
            return sb.ToString();
        }

    }

}
