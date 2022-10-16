using Domain.Entities;
using Domain.Repositories;

namespace DataAccess.EF.Repositories.Wrappers;

internal class GroupRepositoryMetricsWrapper : BaseRepositoryMetricsWrapper<Group>, IGroupRepository
{
    private readonly IGroupRepository _repository;

    public GroupRepositoryMetricsWrapper(IGroupRepository repository) : base(repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<Group?> GetByExternalId(Guid externalId, CancellationToken cancellationToken)
    {
        using var _ = GetTimer(nameof(GetByExternalId));

        return await _repository.GetByExternalId(externalId, cancellationToken);
    }

    public async Task<List<Group>> GetByExternalIds(ICollection<Guid> externalIds, CancellationToken cancellationToken)
    {
        using var _ = GetTimer(nameof(GetByExternalId));
        
        return await _repository.GetByExternalIds(externalIds, cancellationToken);
    }
}
