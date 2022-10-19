namespace Hit2
{
    public class Hit
    {
        private readonly List<TestNode> _testNodes = new ();

        public TestNode Do(string testName)
        {
            if (_testNodes.Any(e => e.TestName.Equals(testName)))
            {
                throw new ArgumentException($"duplicated named test node: {testName}");
            }

            var retVal = new TestNode(testName);
            _testNodes.Add(retVal);
            return retVal;
        }

        public override string ToString()
        {
            var pv = new PrintTestNodeVisitor();
            foreach (var node in _testNodes)
            {
                pv.Visit(node);
            }

            return pv.ToString();
        }

    }
    
}
