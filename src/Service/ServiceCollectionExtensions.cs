using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Service.Commands.CreateOrUpdateGroup;
using Service.Commands.CreateOrUpdateSentence;
using Service.Commands.CreateOrUpdateUser;
using Service.Commands.CreateOrUpdateWords;
using Service.Commands.DeleteGroup;
using Service.Commands.DeleteSentences;
using Service.Commands.DeleteWords;

namespace Service;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection source) => 
        source
            .AddMediatR(typeof(ServiceCollectionExtensions))
            .AddScoped<CreateOrUpdateUserCommandValidator>()
            .AddScoped<CreateOrUpdateGroupCommandValidator>()
            .AddScoped<CreateOrUpdateWordListCommandValidator>()
            .AddScoped<CreateOrUpdateWordCommandValidator>()
            .AddScoped<CreateOrUpdateSentenceListCommandHandlerValidator>()
            .AddScoped<CreateOrUpdateSentenceCommandHandlerValidator>()
            .AddScoped<DeleteSentenceListCommandHandlerValidator>()
            .AddScoped<DeleteWordListCommandHandlerValidator>()
            .AddScoped<DeleteGroupListCommandHandlerValidator>();
}
