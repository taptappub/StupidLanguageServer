using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccess.EF.Repositories;

class UserRepository : BaseRepository<User>, IUserRepository
{
    private readonly ILogger<UserRepository> _logger;

    public UserRepository(DbContext context, ILoggerFactory loggerFactory) : base(context)
    {
        if (loggerFactory is null) throw new ArgumentNullException(nameof(loggerFactory));
        
        _logger = loggerFactory.CreateLogger<UserRepository>();
    }

    public Task<User?> GetById(long id, CancellationToken cancellationToken) =>
        Query.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public Task<User?> GetByExternalId(string externalId, CancellationToken cancellationToken) =>
        Query.FirstOrDefaultAsync(x => x.ExternalId == externalId, cancellationToken);
}
