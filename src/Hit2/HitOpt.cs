using Microsoft.Extensions.DependencyInjection;

namespace Hit2
{
    public sealed class HitOpt
    {
        public string? ConfigName { get; set; }
        public readonly IServiceCollection Services = new ServiceCollection();
        public bool RelaxMode { get; set; }
    }
}
