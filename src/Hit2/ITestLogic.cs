namespace Hit2
{
    public interface ITestLogic
    {
        string Name { get; }

        void Arrange(World world, TestNode node, TestRecord record);
        Task ActAsync(World world, TestNode node, TestRecord record);
        void Assert(World world, TestNode node, TestRecord record);
    }
}
