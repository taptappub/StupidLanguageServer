using Domain.Entities;

namespace Domain.Repositories;

public interface IWordRepository: IBaseRepository<Word>
{
    Task<List<Word>> GetGroupWords(Guid groupExternalId, long? lastId, int limit, CancellationToken cancellationToken);

    Task<List<Word>> GetWordsByExternalIds(IReadOnlyCollection<Guid> externalIds, CancellationToken cancellationToken);
}
