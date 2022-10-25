namespace Hit2.TestNodeVisitors
{
    public sealed class FindUnitTestNodeVisitor : AbstractTestNodeVisitor
    {
        private List<TestNode> _testNodes = new();

        protected override void DoVisit(TestNode node, int level)
        {
            if (node.IsUnitTest) _testNodes.Add(node);
        }

        public IEnumerable<TestNode> TestNodes => _testNodes.AsReadOnly();

        public void Clear() => _testNodes.Clear();

    }

}
