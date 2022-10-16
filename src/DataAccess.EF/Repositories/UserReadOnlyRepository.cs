using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccess.EF.Repositories;

class UserReadOnlyRepository : BaseReadOnlyRepository<User>, IUserReadOnlyRepository
{
    private readonly ILogger<UserReadOnlyRepository> _logger;

    public UserReadOnlyRepository(DbContext context, ILoggerFactory loggerFactory) : base(context)
    {
        if (loggerFactory is null) throw new ArgumentNullException(nameof(loggerFactory));

        _logger = loggerFactory.CreateLogger<UserReadOnlyRepository>();
    }

    public Task<User?> GetById(long id, CancellationToken cancellationToken) => Query
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public Task<User?> GetByExternalId(string externalId, CancellationToken cancellationToken) => Query
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.ExternalId == externalId, cancellationToken);
}
