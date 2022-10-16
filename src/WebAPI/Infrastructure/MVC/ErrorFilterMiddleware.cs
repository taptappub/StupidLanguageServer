using Infrastructure.Exceptions;
using WebAPI.ViewModels;

namespace WebAPI.Infrastructure.MVC;

internal class ErrorFilterMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorFilterMiddleware> _logger;

    public ErrorFilterMiddleware(
        RequestDelegate next,
        ILogger<ErrorFilterMiddleware> logger)
    {
        _next = next;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationErrorException ex)
        {
            var model = new ValidationErrorViewModel
            {
                Message = ex.Message,
                ValidationResults = ex.ValidationResults
            };

            context.Response.StatusCode = ex.HttpStatus;
            await context.Response.WriteAsJsonAsync(model);
        }
        catch (HttpException ex)
        {
            var model = new ErrorResponseViewModel(ex);

            context.Response.StatusCode = ex.HttpStatus;
            await context.Response.WriteAsJsonAsync(model);
        }
        catch(Exception ex)
        {
            var model = new ErrorResponseViewModel(ex);

            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(model);
        }
    }
}
