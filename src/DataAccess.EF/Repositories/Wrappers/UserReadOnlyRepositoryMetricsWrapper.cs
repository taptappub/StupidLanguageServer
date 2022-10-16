using Domain.Entities;
using Domain.Repositories;
using Prometheus;

namespace DataAccess.EF.Repositories.Wrappers;

internal class UserReadOnlyRepositoryMetricsWrapper : BaseReadOnlyRepositoryMetricsWrapper<User>, IUserReadOnlyRepository
{
    private readonly IUserReadOnlyRepository _repository;

    public UserReadOnlyRepositoryMetricsWrapper(IUserReadOnlyRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<User?> GetById(long id, CancellationToken cancellationToken)
    {
        using var _ = GetTimer(nameof(GetById));

        return await _repository.GetById(id, cancellationToken);
    }
    
    public async Task<User?> GetByExternalId(string externalId, CancellationToken cancellationToken)
    {
        using var _ = GetTimer(nameof(GetByExternalId));

        return await _repository.GetByExternalId(externalId, cancellationToken);
    }
}
