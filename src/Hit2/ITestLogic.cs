namespace Hit2
{
    public interface ITestLogic
    {
        string Name { get; }

        void Arrange(Claims claims, TestNode node, TestRecord record);
        Task ActAsync(Claims claims, TestNode node, TestRecord record);
        void Assert(Claims claims, TestRecord record);
        void EditClaims(Claims claims, TestRecord record);
    }
}
