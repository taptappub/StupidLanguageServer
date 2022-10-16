using Domain.Entities;

namespace Domain.Repositories;

public interface IGroupReadOnlyRepository
{
    Task<Group?> GetByExternalId(Guid externalId, CancellationToken cancellationToken);
   
    Task<List<Group>> GetList(long? lastId, int limit, CancellationToken cancellationToken);
}
