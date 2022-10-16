using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ServiceLocator
{
    private static IServiceProvider? _provider;

    internal static void SetProvider(IServiceProvider provider)
    {
        _provider = provider ?? throw new ArgumentNullException(nameof(provider));
    }

    public static T? Resolve<T>()
    {
        if (_provider is null)
            throw new InvalidOperationException($"Initialize {nameof(ServiceLocator)} first");

        return (T?)_provider.GetService<T>();
    }

    public static T ResolveRequired<T>() where T : notnull
    {
        if (_provider is null) 
            throw new InvalidOperationException($"Initialize {nameof(ServiceLocator)} first");
            
        return (T)_provider.GetRequiredService<T>();
    }
}
