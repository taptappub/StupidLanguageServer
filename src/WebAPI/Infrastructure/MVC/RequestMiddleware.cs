using Microsoft.AspNetCore.Mvc.Controllers;
using Prometheus;

namespace WebAPI.Infrastructure.MVC;

public class RequestMiddleware
{
    private static readonly Counter _requestCounter = Metrics.CreateCounter(
        "x_request_total", "HTTP Requests Total",
        new CounterConfiguration
        {
            LabelNames = new[] { "method", "controller", "action", "status" }
        });
    private static readonly Histogram _requestDuration = Metrics.CreateHistogram(
        "x_request_duration", "HTTP Requests Duration",
        new HistogramConfiguration
        {
            LabelNames = new[] { "method", "controller", "action" }
        });

    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public RequestMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
    {
        if (loggerFactory is null)
            throw new ArgumentNullException(nameof(loggerFactory));

        _next = next ?? throw new ArgumentNullException(nameof(next));
        _logger = loggerFactory.CreateLogger<RequestMiddleware>();
    }

    public async Task Invoke(HttpContext httpContext)
    {
        var path = httpContext.Request.Path.Value ?? string.Empty;
        var method = httpContext.Request.Method;
        var controllerActionDescriptor = httpContext.GetEndpoint()?.Metadata.GetMetadata<ControllerActionDescriptor>();

        var controllerName = controllerActionDescriptor?.ControllerName;
        var actionName = controllerActionDescriptor?.ActionName;
        var trackMetrics = controllerName is not null && actionName is not null;

        int statusCode;

        try
        {
            if (trackMetrics)
            {
                using (_requestDuration.WithLabels(method, controllerName!, actionName!).NewTimer())
                {
                    await _next.Invoke(httpContext);
                }

                statusCode = httpContext.Response.StatusCode;
                _requestCounter.Labels(method, controllerName!, actionName!, statusCode.ToString()).Inc();
            }
            else
            {
                await _next.Invoke(httpContext);
            }
        }
        catch (Exception ex)
        {

            _logger.LogCritical(ex, null);

            if (trackMetrics)
            {
                statusCode = 500;
                _requestCounter.Labels(method, controllerName!, actionName!, statusCode.ToString()).Inc();
            }
            throw;
        }
    }
}
