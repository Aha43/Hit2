using System.Text;

namespace Hit2
{
    public class TestRecords
    {
        public string Name { get; init; }

        private readonly List<TestRecord> _records = new();

        public TestRecords(string name) => Name = name;

        public TestRecords AddRecord(TestRecord record)
        {
            _records.Add(record);
            return this;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Test: {Name}");
            foreach (var record in _records)
            {
                sb.AppendLine($"{record.ToString()}");
            }
            return sb.ToString();
        }

    }

}
