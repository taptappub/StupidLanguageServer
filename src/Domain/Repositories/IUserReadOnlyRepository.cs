using Domain.Entities;

namespace Domain.Repositories;

public interface IUserReadOnlyRepository
{
    Task<User?> GetById(long id, CancellationToken cancellationToken);
    
    Task<User?> GetByExternalId(string externalId, CancellationToken cancellationToken);
}
