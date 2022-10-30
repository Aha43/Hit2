using Hit2.Exceptions;
using Hit2.TestNodeVisitors;
using System.Text;

namespace Hit2
{
    public class Hit
    {
        private readonly List<TestNode> _testNodes = new();

        private readonly List<UnitTest> _unitTests = new();

        private readonly (List<ITestLogic> testLogics, List<ITearDown> tearDowns) _implementations;

        private readonly HitOpt _opt;

        #region Creation
        public Hit(Action<HitOpt>? setOptions = null)
        {
            var opt = new HitOpt();
            setOptions?.Invoke(opt);
            _opt = opt;

            _implementations = _opt.Services.BuildImplementations<ITestLogic, ITearDown>();
            
            GetTests();
        }

        private void GetTests()
        {
            if (_opt.TestDefiner != null)
            {
                _opt.TestDefiner.Invoke(this);
            }

            var visitor = new FindUnitTestNodeVisitor();
            _testNodes.ForEach(e => visitor.Visit(e));

            foreach (var leaf in visitor.TestNodes)
            {
                var path = leaf.GetPath();
                var unitTest = new UnitTest(path);
                _unitTests.Add(unitTest);
            }
        }
        #endregion

        #region RunTests
        public async Task<TestResults> RunTestsAsync()
        {
            var retVal = new TestResults();
            foreach (var test in _unitTests)
            {
                var result = await RunTestAsync(test.Name).ConfigureAwait(false);
                retVal.AddTestResult(result);
            }

            return retVal;
        }

        public async Task<TestResult> RunTestAsync(string name)
        {
            var test = _unitTests.Where(t => t.Name == name).FirstOrDefault();
            if (test == null)
            {
                throw new TestNotFoundException(name);
            }

            foreach (var t in _implementations.tearDowns)
            {
                await t.TearDownAsync().ConfigureAwait(false);
            }

            var claims = new Claims();

            var records = new TestRecords(name);

            foreach (var node in test.Path)
            {
                var record = new TestRecord(node.Name);

                var testLogic = _implementations.testLogics.FirstOrDefault(e => e.Name.Equals(node.Name));
                if (testLogic != null)
                {
                    try
                    {
                        await testLogic.ArrangeAsync(claims, node.TestData, record).ConfigureAwait(false);
                        await testLogic.ActAsync(claims, node.TestData, record).ConfigureAwait(false);
                        await testLogic.AssertAsync(claims, node.TestData, record).ConfigureAwait(false);
                        await testLogic.EditClaimsAsync(claims, node.TestData, record).ConfigureAwait(false);
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
        #endregion

        #region DefineTestsAndQuaryAbout
        public TestNode Do(string testName)
        {
            var retVal = new TestNode(testName);
            _testNodes.Add(retVal);
            return retVal;
        }

        public TestNode Do<T>() where T : class => Do(typeof(T).Name);

        public IEnumerable<string> TestNames => _unitTests.Select(n => n.Name);
        #endregion

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var ut in _unitTests) sb.Append(ut.ToString());
            return sb.ToString();
        }

    }

}
