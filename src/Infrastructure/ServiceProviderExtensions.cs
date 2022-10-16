using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ServiceProviderExtensions
{
    public static IServiceProvider InitializeServiceLocator(this IServiceProvider source)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));

        ServiceLocator.SetProvider(source);
        return source;
    }
}
