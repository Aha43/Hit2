using Hit2.Exceptions;

namespace Hit2
{
    public sealed class TestNode
    {
        public string Name { get; init; }

        public string Description { get; private set; } = string.Empty;

        private string? _unitTest;
        public string UnitTest => string.IsNullOrWhiteSpace(_unitTest) ? Name : _unitTest;

        public bool IsUnitTest => !string.IsNullOrWhiteSpace(_unitTest) || IsLeaf;

        public TestNode? Parent { get; init; }

        private readonly List<TestNode> _children = new();

        internal TestData TestData => new();

        internal TestNode(
            string testName, 
            TestNode? parent = null)
        {
            Name = testName;
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

        public TestNode Do<T>() where T : class => Do(typeof(T).Name);

        public TestNode With(string name, object value)
        {
            TestData.Set(name, value);
            return this;
        }

        public TestNode And => this;
        public TestNode Then => this;

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

            throw new TestNodeNotFoundException(name);
        }

        public TestNode From<T>() where T : class => From(typeof(T).Name);

        public TestNode AsUserStory(string name) => AsUnitTest(name);

        public TestNode AsUnitTest(string name)
        {
            _unitTest = name;
            return this;
        }

        public TestNode WithDescription(string description)
        {
            Description = description;
            return this;
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

    }

}
