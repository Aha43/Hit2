namespace Hit2
{
    public abstract class AbstractTestLogic : ITestLogic
    {
        public string Name { get; init; }

        protected AbstractTestLogic(string name)
        {
            Name = name;
        }

        public abstract Task PerformTestAsync(World world);
    }
}
