using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccess.EF.Repositories;

class WordReadOnlyRepository : BaseReadOnlyRepository<Word>, IWordReadOnlyRepository
{
    private readonly ILogger<WordReadOnlyRepository> _logger;

    public WordReadOnlyRepository(DbContext context, ILoggerFactory loggerFactory) : base(context)
    {
        if (loggerFactory is null) throw new ArgumentNullException(nameof(loggerFactory));
        
        _logger = loggerFactory.CreateLogger<WordReadOnlyRepository>();
    }

    public Task<List<Word>> GetGroupWords(Guid groupExternalId, long? lastId, int limit, CancellationToken cancellationToken) =>
        Query
            .Where(x => x.Group.ExternalId == groupExternalId && (!lastId.HasValue || x.Id > lastId))
            .OrderBy(x => x.Id)
            .Take(limit)
            .ToListAsync();

    public Task<List<Word>> GetWordByExternalIds(ICollection<Guid> groupExternalIds, CancellationToken cancellationToken) =>
        Query
            .Where(x => groupExternalIds.Contains(x.Group.ExternalId))
            .ToListAsync();

    protected override IQueryable<Word> ApplyFilter(IQueryable<Word> query)
    {
        var userId = AuthorizationContext.CurrentUser?.Id;

        return userId is not null
            ? query.Where(x => x.User.Id == userId)
            : query.Where(x => false);
    }
}
