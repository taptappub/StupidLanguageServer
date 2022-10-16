using DataAccess.EF.Impl;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DataAccess.EF;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataAccess(this IServiceCollection source, IConfiguration configuration)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (configuration is null) throw new ArgumentNullException(nameof(configuration));

        var connectionString = configuration.GetConnectionString("DataDb");

        return source.AddSingleton<IUnitOfWorkFactory>(sp =>
            new UnitOfWorkFactory(connectionString, sp.GetRequiredService<ILoggerFactory>()));
    }
}
