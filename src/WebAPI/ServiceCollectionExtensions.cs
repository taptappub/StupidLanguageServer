using Serilog;
using WebAPI.Infrastructure;
using WebAPI.Infrastructure.MVC;

namespace WebAPI;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuthorization(this IServiceCollection source, IConfiguration configuration)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (configuration is null) throw new ArgumentNullException(nameof(configuration));

        var authConfig = new AuthorizationConfiguration();
        configuration.GetSection("Authorization").Bind(authConfig);

        source.AddSingleton<IAuthorizationConfiguration>(authConfig);

        return source;
    }

    public static IServiceCollection AddLogger(this IServiceCollection source, IConfiguration configuration)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (configuration is null) throw new ArgumentNullException(nameof(configuration));

        ConfigLogger(configuration);

        source
            .AddLogging(builder => builder.AddSerilog(dispose: true))
            .AddScoped<UtcTimestampColumnWriter>();

        return source;
    }

    private static void ConfigLogger(IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(configuration)
        .Enrich.FromLogContext()
        .CreateLogger();
    }
}
