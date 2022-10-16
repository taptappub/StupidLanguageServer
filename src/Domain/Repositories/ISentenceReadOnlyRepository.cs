using Domain.Entities;

namespace Domain.Repositories;

public interface ISentenceReadOnlyRepository
{
    Task<List<Sentence>> GetSentencePage(long? lastId, int limit, CancellationToken cancellationToken);
}
