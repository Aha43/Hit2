namespace Hit2.TestNodeVisitors
{
    public sealed class FindLeafTestNodeVisitor : AbstractTestNodeVisitor
    {
        private List<TestNode> _leafs = new();

        protected override void DoVisit(TestNode node, int level)
        {
            if (node.IsLeaf) _leafs.Add(node);
        }

        public IEnumerable<TestNode> Leafs => _leafs.AsReadOnly();

        public void Clear() => _leafs.Clear();

    }

}
