using System.Net;
using Infrastructure.Authorization;
using Infrastructure.Logging;
using MediatR;
using Microsoft.AspNetCore.Http.Features;
using Service.Dto;
using Service.Queries;
using Service.Queries.GetUserByExternalId;

namespace WebAPI.Infrastructure.MVC;

internal class AutorizationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IMediator _mediator;
    private readonly ILogger<AutorizationMiddleware> _logger;
    private readonly IAuthorizationConfiguration _configuration;

    public AutorizationMiddleware(
        RequestDelegate next,
        IMediator mediator,
        ILogger<AutorizationMiddleware> logger,
        IAuthorizationConfiguration configuration)
    {
        _next = next;
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var endpoint = context.Features.Get<IEndpointFeature>()?.Endpoint;
        var attribute = endpoint?.Metadata.GetMetadata<AuthorizeUserAttribute>();
        if (attribute is null || attribute.Required == false)
        {
            await _next(context);
            return;
        }

        UserDto? userDto;
        using var requestLogScope = _logger.SetProperty("RequestIP", context.Connection.RemoteIpAddress?.ToString());

        userDto = await GetUserDto(context);

        if (userDto is not null)
        {
            var authUser = new AuthorizedUser(userDto.Id, userDto.ExternalId);
            using var authLogScope = _logger.SetProperty("Authorization", authUser, true);

            AuthorizationContext.SetUser(authUser);
            // Call the next delegate/middleware in the pipeline.
            await _next(context);
            AuthorizationContext.ResetUser();
        }
        else
        {
            _logger.LogError("Request unauthorized");

            context.Response.Clear();
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            await context.Response.WriteAsync("Unauthorized");
        }
    }

    private string? GetAuthUserId(HttpContext context)
    {
        var headers = context.Request.Headers;

        if (!string.IsNullOrEmpty(_configuration.Header)
            && headers.TryGetValue(_configuration.Header, out var authValue)
            && authValue.Count == 1)
        {
            return authValue[0];
        }

        return null;
    }

    private async Task<UserDto?> GetUserDto(HttpContext context)
    {
        var externalId = GetAuthUserId(context);
        if (externalId is null)
        {
            return null;
        }

        using var scope = _logger.BeginScope("User authorization");
        using var propScope = _logger.SetProperty("UserExternalId", externalId);
        
        return await _mediator.Send(new GetUserByExternalIdQuery(externalId));
    }
}
