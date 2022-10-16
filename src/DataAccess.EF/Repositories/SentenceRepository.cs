using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccess.EF.Repositories;

class SentenceRepository : BaseRepository<Sentence>, ISentenceRepository
{
    private readonly ILogger<SentenceRepository> _logger;

    public SentenceRepository(DbContext context, ILoggerFactory loggerFactory) : base(context)
    {
        if (loggerFactory is null) throw new ArgumentNullException(nameof(loggerFactory));

        _logger = loggerFactory.CreateLogger<SentenceRepository>();
    }

    public Task<List<Sentence>> GetSentencesByExternalIds(IReadOnlyCollection<Guid> externalIds, CancellationToken cancellationToken) =>
        Query
            .Where(x => externalIds.Contains(x.ExternalId))
            .ToListAsync();
}
