using Domain.Entities;

namespace Domain.Repositories;

public interface ISentenceRepository : IBaseRepository<Sentence>
{
    Task<List<Sentence>> GetSentencesByExternalIds(IReadOnlyCollection<Guid> externalIds, CancellationToken cancellationToken);
}
