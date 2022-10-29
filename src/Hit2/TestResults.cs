using System.Text;

namespace Hit2
{
    public sealed class TestResults
    {
        private readonly List<TestResult> _results = new();

        internal void AddTestResult(TestResult testResult) => _results.Add(testResult);

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var testResult in _results)
            {
                sb.Append(testResult.ToString());
            }
            return sb.ToString();
        }

    }

}
