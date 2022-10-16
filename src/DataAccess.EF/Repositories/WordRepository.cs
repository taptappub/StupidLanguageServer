using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccess.EF.Repositories;

class WordRepository : BaseRepository<Word>, IWordRepository
{
    private readonly ILogger<WordRepository> _logger;

    public WordRepository(DbContext context, ILoggerFactory loggerFactory) : base(context)
    {
        if (loggerFactory is null) throw new ArgumentNullException(nameof(loggerFactory));
        
        _logger = loggerFactory.CreateLogger<WordRepository>();
    }

    public Task<List<Word>> GetGroupWords(Guid groupExternalId, long? lastId, int limit, CancellationToken cancellationToken) =>
        Query
            .Where(x => x.Group.ExternalId == groupExternalId && (!lastId.HasValue || x.Id > lastId))
            .OrderBy(x => x.Id)
            .Take(limit)
            .ToListAsync();

    public Task<List<Word>> GetWordsByExternalIds(IReadOnlyCollection<Guid> externalIds, CancellationToken cancellationToken) =>
        Query
            .Where(x => externalIds.Contains(x.ExternalId))
            .ToListAsync();
}
