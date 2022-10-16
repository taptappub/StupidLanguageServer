using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace Infrastructure.Logging;

public static class LoggerExtensions
{
    public static ILogPropertiesScope SetProperty(
        this ILogger logger, 
        string name, 
        object? value, 
        bool destructureValue = false)
    {
        if (logger is null) throw new ArgumentNullException(nameof(logger));
        if (string.IsNullOrEmpty(name)) throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));

        var propScope = new LogPropertiesScope();
        try
        {
            var property = LogContext.PushProperty(name, value, destructureValue);
            propScope.Add(property);
        }
        catch
        {
            propScope.Dispose();
            throw;
        }
        return propScope;
    }

    public static ILogPropertiesScope SetProperty(this ILogger logger, params LogProperty[] properties)
    {
        if (logger is null) throw new ArgumentNullException(nameof(logger));

        var propScope = new LogPropertiesScope();
        try
        {
            foreach (var property in properties)
            {
                var propObject = LogContext.PushProperty(property.Name, property.Value, property.DestructValue);
                propScope.Add(propObject);
            }
        }
        catch
        {
            propScope.Dispose();
            throw;
        }
        return propScope;
    }
}
