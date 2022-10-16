using Domain.Entities;
using Domain.Repositories;

namespace DataAccess.EF.Repositories.Wrappers;

internal class SentenceRepositoryMetricsWrapper : BaseRepositoryMetricsWrapper<Sentence>, ISentenceRepository
{
    private readonly ISentenceRepository _repository;

    public SentenceRepositoryMetricsWrapper(ISentenceRepository repository) : base(repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<List<Sentence>> GetSentencesByExternalIds(IReadOnlyCollection<Guid> externalIds, CancellationToken cancellationToken)
    {
        using var _ = GetTimer(nameof(GetSentencesByExternalIds));

        return await _repository.GetSentencesByExternalIds(externalIds, cancellationToken);
    }
}
