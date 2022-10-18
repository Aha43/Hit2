using System.Text;

namespace Hit2
{
    public sealed class TestNode
    {
        public string TestName { get; init; }

        public TestNode? Parent { get; init; }

        private readonly List<TestNode> _children = new();

        private Dictionary<string, object> _attributes = new();

        internal TestNode(
            string testName, 
            TestNode? parent = null)
        {
            TestName = testName;
            Parent = parent;
        }

        public IEnumerable<TestNode> Children => _children.AsReadOnly();

        public TestNode Do(string testName)
        {
            if (_children.Any(e => e.TestName.Equals(testName)))
            {
                throw new ArgumentException($"duplicated named test node: {testName}");
            }

            var retVal = new TestNode(testName, this);
            _children.Add(retVal);
            return retVal;
        }

        public TestNode With(string name, object value)
        {
            _attributes[name] = value;
            return this;
        }

        public TestNode From(string testName)
        {
            var current = this;
            while (current != null)
            {
                if (current.TestName.Equals(testName))
                {
                    return current;
                }
                current = current.Parent;
            }

            throw new ArgumentException($"ancestor test node named {testName} not found");
        }

        public override string ToString()
        {
            var sb = new StringBuilder($"Do: {TestName}");

            return sb.ToString();
        }

    }

}
