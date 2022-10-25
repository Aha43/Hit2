using Microsoft.Extensions.DependencyInjection;

namespace Hit2
{
    public static class ReflUtil
    {
        public static List<Type> FindImplementingTypes<T>() where T : class
        {  
            var type = typeof(T);
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && !p.IsAbstract)
                .ToList();
        }

        public static List<T> CreateImplementations<T>(List<T>? result = null) where T : class
        {
            result ??= new();

            var types = FindImplementingTypes<T>();
            foreach (var t in types)
            {
                if (Activator.CreateInstance(t) is T implementation) result.Add(implementation);
            }

            return result;
        }

        public static List<Type> AddImplementations<T>(this IServiceCollection services, List<Type>? types = null) where T : class
        {
            types ??= new();
            types.AddRange(FindImplementingTypes<T>());
            foreach (var t in types) services.AddSingleton(t);
            return types;
        }

        public static List<T> BuildImplementations<T>(this IServiceCollection services) where T : class
        {
            var types = services.AddImplementations<T>();
            var serviceBuilder = services.BuildServiceProvider();
            return types.Select(t => (T)serviceBuilder.GetRequiredService(t)).ToList();
        }

        public static (List<T1>, List<T2>) BuildImplementations<T1, T2>(this IServiceCollection services) where T1 : class where T2 : class
        {
            var types1 = services.AddImplementations<T1>();
            var types2 = services.AddImplementations<T2>();
            
            var serviceBuilder = services.BuildServiceProvider();
            
            var l1 = types1.Select(t => (T1)serviceBuilder.GetRequiredService(t)).ToList();
            var l2 = types2.Select(t => (T2)serviceBuilder.GetRequiredService(t)).ToList();

            return (l1, l2);
        }

    }

}
