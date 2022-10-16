using Domain.Entities;
using Domain.Repositories;

namespace DataAccess.EF.Repositories.Wrappers;

internal class WordRepositoryMetricsWrapper : BaseRepositoryMetricsWrapper<Word>, IWordRepository
{
    private readonly IWordRepository _repository;

    public WordRepositoryMetricsWrapper(IWordRepository repository) : base(repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<List<Word>> GetGroupWords(Guid groupExternalId, long? lastId, int limit, CancellationToken cancellationToken)
    {
        using var _ = GetTimer(nameof(GetGroupWords));

        return await _repository.GetGroupWords(groupExternalId, lastId, limit, cancellationToken);
    }


    public async Task<List<Word>> GetWordsByExternalIds(IReadOnlyCollection<Guid> externalIds, CancellationToken cancellationToken)
    {
        using var _ = GetTimer(nameof(GetGroupWords));

        return await _repository.GetWordsByExternalIds(externalIds, cancellationToken);
    }
}
