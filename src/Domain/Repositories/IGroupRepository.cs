using Domain.Entities;

namespace Domain.Repositories;

public interface IGroupRepository : IBaseRepository<Group>
{
    Task<Group?> GetByExternalId(Guid externalId, CancellationToken cancellationToken);

    Task<List<Group>> GetByExternalIds(ICollection<Guid> externalIds, CancellationToken cancellationToken);
}
