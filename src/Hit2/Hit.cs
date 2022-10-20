using Hit2.TestNodeVisitors;
using System.Text;

namespace Hit2
{
    public class Hit
    {
        private readonly List<TestNode> _testNodes = new();

        private readonly List<UnitTest> _unitTests;

        public Hit()
        {
            foreach (var definer in FindTestDefiners())
            {
                definer.Define(this);
            }

            _unitTests = GetTests();
        }

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
            //var pv = new PrintTestNodeVisitor();
            //Dfs(pv);
            //return pv.ToString();
            var sb = new StringBuilder();
            foreach (var ut in _unitTests)
            {
                sb.Append(ut.ToString());
            }

            return sb.ToString();
        }

        private void Dfs(AbstractTestNodeVisitor visitor)
        {
            foreach (var node in _testNodes) visitor.Visit(node);
        }

        #region PrivateMethods

        private static List<ITestDefiner> FindTestDefiners()
        {
            List<ITestDefiner> result = new();

            var type = typeof(ITestDefiner);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p));

            foreach (var t in types)
            {
                if (!t.IsAbstract)
                {
                    if (Activator.CreateInstance(t) is ITestDefiner definer)
                    {
                        result.Add(definer);
                    }
                }
            }

            return result;
        }

        private List<UnitTest> GetTests()
        {
            var result = new List<UnitTest>();

            var visitor = new FindLeafTestNodeVisitor();
            Dfs(visitor);

            foreach (var leaf in visitor.Leafs)
            {
                var path = leaf.GetPath();
                var unitTest = new UnitTest(path);
                result.Add(unitTest);
            }

            return result;
        }

        #endregion

    }

}
