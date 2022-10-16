using Domain.Entities;
using Domain.Repositories;
using Prometheus;

namespace DataAccess.EF.Repositories.Wrappers;

internal class WordReadOnlyRepositoryMetricsWrapper : BaseReadOnlyRepositoryMetricsWrapper<Word>, IWordReadOnlyRepository
{
    private readonly IWordReadOnlyRepository _repository;

    public WordReadOnlyRepositoryMetricsWrapper(IWordReadOnlyRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<List<Word>> GetGroupWords(Guid groupExternalId, long? lastId, int limit, CancellationToken cancellationToken)
    {
        using var _ = GetTimer(nameof(GetGroupWords));

        return await _repository.GetGroupWords(groupExternalId, lastId, limit, cancellationToken);
    }

    public async Task<List<Word>> GetWordByExternalIds(ICollection<Guid> groupExternalIds, CancellationToken cancellationToken)
    {
        using var _ = GetTimer(nameof(GetWordByExternalIds));

        return await _repository.GetWordByExternalIds(groupExternalIds, cancellationToken);
    }
}
