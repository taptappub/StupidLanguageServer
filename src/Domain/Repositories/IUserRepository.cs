using Domain.Entities;

namespace Domain.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetById(long id, CancellationToken cancellationToken);
    
    Task<User?> GetByExternalId(string externalId, CancellationToken cancellationToken);
}
