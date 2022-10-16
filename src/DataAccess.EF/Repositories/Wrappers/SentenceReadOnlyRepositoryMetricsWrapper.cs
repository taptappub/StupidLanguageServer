using Domain.Entities;
using Domain.Repositories;
using Prometheus;

namespace DataAccess.EF.Repositories.Wrappers;

internal class SentenceReadOnlyRepositoryMetricsWrapper : BaseReadOnlyRepositoryMetricsWrapper<Sentence>, ISentenceReadOnlyRepository
{
    private readonly ISentenceReadOnlyRepository _repository;

    public SentenceReadOnlyRepositoryMetricsWrapper(ISentenceReadOnlyRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<List<Sentence>> GetSentencePage(long? lastId, int limit, CancellationToken cancellationToken)
    {
        using var _ = GetTimer(nameof(GetSentencePage));

        return await _repository.GetSentencePage(lastId, limit, cancellationToken);
    }
}
