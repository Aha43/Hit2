using Hit2;

namespace Hit2Cli
{
    public class CreatePersonTestLogic : TestLogicBase
    {
        public override Task ActAsync(World world, TestNode node, TestRecord record)
        {
            record.WriteLine(node.ToString());
            return Task.CompletedTask;
        }

    }

}
