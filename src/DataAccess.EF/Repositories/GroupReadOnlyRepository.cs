using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccess.EF.Repositories;

class GroupReadOnlyRepository : BaseReadOnlyRepository<Group>, IGroupReadOnlyRepository
{
    private readonly ILogger<GroupReadOnlyRepository> _logger;

    public GroupReadOnlyRepository(DbContext context, ILoggerFactory loggerFactory) : base(context)
    {
        if (loggerFactory is null) throw new ArgumentNullException(nameof(loggerFactory));

        _logger = loggerFactory.CreateLogger<GroupReadOnlyRepository>();
    }

    public Task<Group?> GetByExternalId(Guid externalId, CancellationToken cancellationToken) => Query
        .Where(x => x.ExternalId == externalId)
        .AsNoTracking()
        .FirstOrDefaultAsync();


    public Task<List<Group>> GetList(long? lastId, int limit, CancellationToken cancellationToken) =>
        Query.Where(x => !lastId.HasValue || x.Id > lastId.Value)
            .AsNoTracking()
            .Take(limit)
            .ToListAsync();
}
