using Domain.Entities;
using Domain.Repositories;

namespace DataAccess.EF.Repositories.Wrappers;

internal class GroupReadOnlyRepositoryMetricsWrapper : BaseReadOnlyRepositoryMetricsWrapper<Group>, IGroupReadOnlyRepository
{
    private readonly IGroupReadOnlyRepository _repository;

    public GroupReadOnlyRepositoryMetricsWrapper(IGroupReadOnlyRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<Group?> GetByExternalId(Guid externalId, CancellationToken cancellationToken)
    {
        using var _ = GetTimer(nameof(GetByExternalId));

        return await _repository.GetByExternalId(externalId, cancellationToken);
    }

    public async Task<List<Group>> GetList(long? lastId, int limit, CancellationToken cancellationToken)
    {
        {
            using var _ = GetTimer(nameof(GetList));

            return await _repository.GetList(lastId, limit, cancellationToken);
        }
    }
}
