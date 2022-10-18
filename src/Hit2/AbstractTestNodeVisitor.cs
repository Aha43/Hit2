namespace Hit2
{
    public abstract class AbstractTestNodeVisitor
    {
        public void Visit(TestNode node, int level = 0)
        {
            DoVisit(node, level);
            foreach (var child in node.Children)
            {
                Visit(child, level + 1);
            }
        }

        protected virtual void DoVisit(TestNode node, int level) { }

    }

}
