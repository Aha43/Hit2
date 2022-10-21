namespace Hit2
{
    public interface ITestLogic
    {
        string Name { get; }
        Task PerformTestAsync(World world);
    }
}
