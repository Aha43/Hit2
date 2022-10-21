using System.Text;

namespace Hit2
{
    public class UnitTest
    {
        private readonly TestNode[] _path;

        internal UnitTest(TestNode[] path) => _path = path;

        internal TestNode[] Path => _path;

        public string Name => _path[^1].UnitTest;

        public override string ToString()
        {
            var sb = new StringBuilder($"{Name}:");
            sb.AppendLine();
            foreach (var item in _path)
            {
                sb.AppendLine($"  {item.ToString()}");
            }

            return sb.ToString();
        }
    }
}
