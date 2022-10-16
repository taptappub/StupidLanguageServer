using System.Reflection;
using Infrastructure;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebAPI.Infrastructure.MVC;

namespace WebAPI.Infrastructure.SwaggerExtensions;

public class AuthorizationHeaderSwaggerAttribute : IOperationFilter
{
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var authRequested = HasAuthAttribute(context.MethodInfo) || HasAuthAttribute(context.MethodInfo.DeclaringType);
        if (authRequested)
        {
            var authConfiguration = ServiceLocator.ResolveRequired<IAuthorizationConfiguration>();
            operation.Parameters.Add(new OpenApiParameter
            {
                In = ParameterLocation.Header,
                Schema = new OpenApiSchema { Type = "string" },
                Name = authConfiguration.Header,
                Required = true
            });
        }
    }

    private static bool HasAuthAttribute(MethodInfo methodInfo)
    {
        var authAttribute = methodInfo
            .GetCustomAttributes(typeof(AuthorizeUserAttribute), true)
            .Cast<AuthorizeUserAttribute>();

        return authAttribute is not null && authAttribute.Any();
    }

    private static bool HasAuthAttribute(Type? controllerType)
    {
        var authAttribute = controllerType
            ?.GetCustomAttributes(typeof(AuthorizeUserAttribute), true)
            .Cast<AuthorizeUserAttribute>();

        return authAttribute is not null && authAttribute.Any();
    }
}
