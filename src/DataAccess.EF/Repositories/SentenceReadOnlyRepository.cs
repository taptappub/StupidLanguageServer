using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccess.EF.Repositories;

class SentenceReadOnlyRepository : BaseReadOnlyRepository<Sentence>, ISentenceReadOnlyRepository
{
    private readonly ILogger<SentenceReadOnlyRepository> _logger;

    public SentenceReadOnlyRepository(DbContext context, ILoggerFactory loggerFactory) : base(context)
    {
        if (loggerFactory is null) throw new ArgumentNullException(nameof(loggerFactory));

        _logger = loggerFactory.CreateLogger<SentenceReadOnlyRepository>();
    }

    public Task<List<Sentence>> GetSentencePage(long? lastId, int limit, CancellationToken cancellationToken) =>
        Query.Where(x => (lastId == null || x.Id > lastId))
            .OrderBy(x => x.Id)
            .Include(x => x.Words).ThenInclude(x => x.Word)
            .Take(limit)
            .AsSplitQuery()
            .ToListAsync(cancellationToken);
}
