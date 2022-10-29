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

        public virtual void Arrange(Claims claims, TestNode node, TestRecord record) { }
        public virtual Task ActAsync(Claims claims, TestNode node, TestRecord record) => Task.CompletedTask; 
        public virtual void Assert(Claims claims, TestRecord record) { }
        public virtual void EditClaims(Claims claims, TestRecord record) { }

    }

}
