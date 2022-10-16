using DataAccess.EF;
using Infrastructure;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Prometheus;
using Service;
using WebAPI.Infrastructure.MVC;
using WebAPI.Infrastructure.SwaggerExtensions;

namespace WebAPI;

public static class Program
{
        public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Logging.ClearProviders();

        // Add services to the container.
        builder.Services.AddLogger(builder.Configuration);
        builder.Services.AddDataAccess(builder.Configuration);
        builder.Services.AddServices();
        builder.Services.AddControllers(x =>
        {
            x.AllowEmptyInputInBodyModelBinding = true;
        });
        builder.Services.AddAuthorization(builder.Configuration);
        builder.Services.AddApiVersioning(options =>
        {
            options.ReportApiVersions = true;
        });
        builder.Services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

        //var apiVersionDescriptionProvider = builder.Services.GetRequiredService<IApiVersionDescriptionProvider>();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.OperationFilter<AuthorizationHeaderSwaggerAttribute>();
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "WebAPI.xml"));
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Service.xml"));
        });
        builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();
        
        var app = builder.Build();
        
        RegisteMiddlewares(app);
        app.Services.InitializeServiceLocator();

        // Configure the HTTP request pipeline.
        //if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
                // build a swagger endpoint for each discovered API version
                foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
            });
        }

        app.UseHttpsRedirection();
        app.UseMetricServer();  

        app.MapControllers();

        app.Run();
    }

    private static void RegisteMiddlewares(IApplicationBuilder builder)
    {
        builder.UseMiddleware<RequestMiddleware>();
        builder.UseMiddleware<ErrorFilterMiddleware>();
        builder.UseMiddleware<AutorizationMiddleware>();        
    }
}