using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccess.EF.Repositories;

class GroupRepository : BaseRepository<Group>, IGroupRepository
{
    private readonly ILogger<GroupRepository> _logger;

    public GroupRepository(DbContext context, ILoggerFactory loggerFactory) : base(context)
    {
        if (loggerFactory is null) throw new ArgumentNullException(nameof(loggerFactory));
        
        _logger = loggerFactory.CreateLogger<GroupRepository>();
    }

    public Task<Group?> GetByExternalId(Guid externalId, CancellationToken cancellationToken) =>
        Query.FirstOrDefaultAsync(x => x.ExternalId == externalId, cancellationToken);

    public Task<List<Group>> GetByExternalIds(ICollection<Guid> externalIds, CancellationToken cancellationToken) =>
        Query.Where(x => externalIds.Contains(x.ExternalId))
            .ToListAsync(cancellationToken);

    protected override IQueryable<Group> ApplyFilter(IQueryable<Group> query)
    {
        var userId = AuthorizationContext.CurrentUser?.Id;

        return userId is not null
            ? query.Where(x => x.User.Id == userId)
            : query.Where(x => false);
    }
}
