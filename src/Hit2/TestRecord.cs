using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit2
{
    public class TestRecord
    {
        public string Name { get; init; }

        private readonly List<string> _info = new List<string>();

        public TestRecord(string name)
        { 
            Name = name; 
        }

        public TestRecord WriteLine(string? line)
        {
            line = line ?? string.Empty;
            _info.Add(line);
            return this;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"  Do : {Name}");
            foreach (var item in _info)
            {
                sb.AppendLine($"    {item.ToString()}");
            }

            return sb.ToString();
        }

    }

}
