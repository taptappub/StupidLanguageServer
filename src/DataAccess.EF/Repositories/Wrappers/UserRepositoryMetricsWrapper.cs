using Domain.Entities;
using Domain.Repositories;

namespace DataAccess.EF.Repositories.Wrappers;

internal class UserRepositoryMetricsWrapper : BaseRepositoryMetricsWrapper<User>, IUserRepository
{
    private readonly IUserRepository _repository;

    public UserRepositoryMetricsWrapper(IUserRepository repository) : base(repository)
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
