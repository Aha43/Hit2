using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hit2
{
    public sealed class HitOpt
    {
        public string? ConfigName { get; set; }
        public bool AddUserSecretToConfig { get; set; }
        public string? AddJsonFileToConfig { get; set; }
        public Action<IServiceCollection, IConfiguration>? AddServices { get; set; }
        public bool RelaxMode { get; set; }
    }
}
