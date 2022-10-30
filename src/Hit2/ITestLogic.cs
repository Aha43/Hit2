namespace Hit2
{
    public interface ITestLogic
    {
        string Name { get; }

        Task ArrangeAsync(Claims claims, TestData data, TestRecord record);
        void Arrange(Claims claims, TestData data, TestRecord record);
        Task ActAsync(Claims claims, TestData data, TestRecord record);
        void Act(Claims claims, TestData data, TestRecord record);
        Task AssertAsync(Claims claims, TestData data, TestRecord record);
        void Assert(Claims claims, TestData data, TestRecord record);
        Task EditClaimsAsync(Claims claims, TestData data, TestRecord record);
        void EditClaims(Claims claims, TestData data, TestRecord record);
    }
}
