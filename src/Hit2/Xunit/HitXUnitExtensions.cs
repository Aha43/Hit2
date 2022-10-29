using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Hit2.Xunit
{
    public static class HitXUnitExtensions
    {
        public static string GenerateXUnitTests(this Hit hit)
        {
            var sb = new StringBuilder();
            var t1 = "  ";
            var t2 = "    ";

            //sb.AppendLine("using Hit2;");
            //sb.AppendLine();
            //sb.AppendLine("public class HitTests");
            //sb.AppendLine("{");
            //sb.AppendLine($"{t1}private readonly Hit _hit;");

            sb.AppendLine($"{t1}[Theory]");
            foreach (var name in hit.TestNames)
            {
                sb.AppendLine($"{t1}[InlineData({name})]");
            }
            sb.AppendLine($"{t1}public async Task HitTest(string testName)");
            sb.AppendLine($"{t1 + '{'}");
            sb.AppendLine($"{t2}await _hit.RunTest(testName).ConfigureAwait(false);");
            sb.AppendLine($"{t1 + '}'}");

            //sb.AppendLine("}");

            return sb.ToString();
        }

    }

}
