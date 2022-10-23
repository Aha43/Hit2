using System.Text;

namespace Hit2
{
    public sealed class TestResult
    {
        public bool Success { get; init; }

        public Exception? Exception { get; init; }

        public string TestName => _records.Name;

        private readonly TestRecords _records;

        internal TestResult(TestRecords records) : this(null, records) { }

        internal TestResult(Exception? exception, TestRecords records)
        {
            Success = exception == null;
            Exception = exception;
            _records = records;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            var state = Success ? "success" : "failed";
            sb.AppendLine($"Result for test {TestName} : {state}");
            sb.AppendLine(_records.ToString());
            if (Exception != null)
            {
                sb.AppendLine(Exception.ToString());
            }
            return sb.ToString();
        }

    }

}
