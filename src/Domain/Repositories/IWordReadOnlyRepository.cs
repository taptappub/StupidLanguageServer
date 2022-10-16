using Domain.Entities;

namespace Domain.Repositories;

public interface IWordReadOnlyRepository
{
    Task<List<Word>> GetGroupWords(Guid groupExternalId, long? lastId, int limit, CancellationToken cancellationToken);

    Task<List<Word>> GetWordByExternalIds(ICollection<Guid> groupExternalIds, CancellationToken cancellationToken);
}
