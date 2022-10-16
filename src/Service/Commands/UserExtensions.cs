using DataAccess.EF;
using Domain.Entities;
using Infrastructure.Authorization;
using Infrastructure.Exceptions;
using Microsoft.Extensions.Logging;

namespace Service.Commands;

internal static class UserExtensions
{
    public static async Task<User> GetAuthorizedUser(this IUnitOfWork uow, ILogger logger, CancellationToken cancellationToken)
    {
        var user = await uow.Repositories.User.GetById(AuthorizationContext.CurrentUser!.Id, cancellationToken);

        if (user is null)
        {
            logger?.LogWarning("User from authorization context not found");
            
            var ex = new ObjectNotFoundException($"Onwed user not found");
            ex.Data.Add($"{nameof(User)}.{nameof(User.Id)}", AuthorizationContext.CurrentUser!.Id);
            ex.Data.Add($"{nameof(User)}.{nameof(User.ExternalId)}", AuthorizationContext.CurrentUser!.ExternalId);

            throw ex;            
        }
        
        return user;
    }
}
