using Hit2.Exceptions;
using Hit2.TestNodeVisitors;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace Hit2
{
    public class Hit
    {
        private readonly List<TestNode> _testNodes = new();

        private readonly List<UnitTest> _unitTests = new();

        private readonly List<Type> _logicTypes = new();

        private readonly Dictionary<string, ITestLogic> _testLogic = new();

        private readonly HitOpt _opt;
        
        public Hit(Action<HitOpt>? setOptions = null)
        {
            var opt = new HitOpt();
            setOptions?.Invoke(opt);
            _opt = opt;
            Initialize();

            var serviceProvider = GetServiceProvider();
            ResolveTestLogic(serviceProvider);
        }

        public async Task<TestResult> RunTestAsync(string name)
        {
            var test = _unitTests.Where(t => t.Name == name).FirstOrDefault();
            if (test == null)
            {
                throw new ArgumentException($"Test named {name} not found");
            }

            var world = new World();

            var records = new TestRecords(name);

            foreach (var node in test.Path)
            {
                var record = new TestRecord(node.Name);
                if (_testLogic.TryGetValue(node.Name, out var testLogic))
                {
                    try
                    {
                        testLogic.Arrange(world, node, record);
                        await testLogic.ActAsync(world, node, record).ConfigureAwait(false);
                        testLogic.Assert(world, node, record);
                    }
                    catch (Exception ex)
                    {
                        return new TestResult(ex, records);
                    }
                }
                else
                {
                    if (_opt.RelaxMode)
                    {
                        record.WriteLine("Missing logic, ignores (relax mode)");
                    }
                    else
                    {
                        throw new TestLogicNotFoundException(name, node.Name);
                    }
                }
                records.AddRecord(record);
            }

            return new TestResult(records);
        }

        private void Initialize()
        {
            foreach (var definer in FindTestDefiners()) definer.Define(this);
            GetTests();
        }

        public TestNode Do(string testName)
        {
            var retVal = new TestNode(testName);
            _testNodes.Add(retVal);
            return retVal;
        }

        public override string ToString()
        {
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

        private IServiceProvider GetServiceProvider()
        {
            AddTestLogicToServices(_opt.Services);

            var serviceProvider = _opt.Services.BuildServiceProvider();
            return serviceProvider;
        }

        private void AddTestLogicToServices(IServiceCollection services)
        {
            FindTestLogic();
            foreach (var t in _logicTypes) services.AddSingleton(t);
        }

        private void FindTestLogic()
        {
            var type = typeof(ITestLogic);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p));

            foreach (var t in types)
            {
                if (!t.IsAbstract)
                {
                    _logicTypes.Add(t);
                }
            }
        }

        private void GetTests()
        {
            var visitor = new FindLeafTestNodeVisitor();
            Dfs(visitor);

            foreach (var leaf in visitor.Leafs)
            {
                AddTest(leaf);
            }
        }

        private void AddTest(TestNode leaf)
        {
            var path = leaf.GetPath();
            var unitTest = new UnitTest(path);
            _unitTests.Add(unitTest);
        }

        private void ResolveTestLogic(IServiceProvider serviceProvider)
        {
            foreach (var t in _logicTypes)
            {
                var logic = serviceProvider.GetRequiredService(t) as ITestLogic;
                if (logic != null)
                {
                    _testLogic.Add(logic.Name, logic);
                }
            }
        }

        #endregion

    }

}
