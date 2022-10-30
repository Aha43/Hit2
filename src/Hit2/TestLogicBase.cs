namespace Hit2
{
    public abstract class TestLogicBase : ITestLogic
    {
        public string Name { get; init; }

        protected TestLogicBase(string? name = null)
        {
            if (name == null)
            {
                name = GetType().Name;
                if (!name.Equals("TestLogic") && name.EndsWith("TestLogic")) name = name[..^9];
            }

            Name = name;
        }

        public virtual Task ArrangeAsync(Claims claims, TestData data, TestRecord record)
        {
            Arrange(claims, data, record);
            return Task.CompletedTask;
        }

        public virtual void Arrange(Claims claims, TestData data, TestRecord record) { }
        
        public virtual Task ActAsync(Claims claims, TestData data, TestRecord record)
        {
            Act(claims, data, record);
            return Task.CompletedTask;
        }
        
        public virtual void Act(Claims claims, TestData data, TestRecord record) { }

        public virtual Task AssertAsync(Claims claims, TestData data, TestRecord record)
        {
            Assert(claims, data, record);
            return Task.CompletedTask;
        }
        
        public virtual void Assert(Claims claims, TestData data, TestRecord record) { }

        public virtual Task EditClaimsAsync(Claims claims, TestData data, TestRecord record)
        {
            EditClaims(claims, data, record);
            return Task.CompletedTask;
        }

        public virtual void EditClaims(Claims claims, TestData data, TestRecord record) { }

    }

}
