using System.Text;

namespace Hit2
{
    public sealed class PrintTestNodeVisitor : AbstractTestNodeVisitor
    {
        private StringBuilder _sb = new();

        protected override void DoVisit(TestNode node, int level)
        {
            for (int i = 0; i < level; i++)
            {
                _sb.Append(' ');
            }

            _sb.AppendLine(node.ToString());
        }

        public override string ToString() => _sb.ToString();

    }

}
