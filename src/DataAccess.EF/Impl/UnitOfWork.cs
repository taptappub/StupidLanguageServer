using Microsoft.Extensions.Logging;

namespace DataAccess.EF.Impl;

class UnitOfWork : IUnitOfWork
{
    private readonly DatabaseContext _context;
    private readonly ILogger<UnitOfWork> _logger;

    public IRepositoryFactory Repositories { get; }

    public UnitOfWork(string connectionString, ILoggerFactory loggerFactory)
    {
        if (string.IsNullOrEmpty(connectionString)) throw new ArgumentException(nameof(connectionString));
        if (loggerFactory is null) throw new ArgumentNullException(nameof(loggerFactory));

        _context = new DatabaseContext(connectionString);
        _logger = loggerFactory.CreateLogger<UnitOfWork>();
        Repositories = new RepositoryFactory(_context, loggerFactory);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken) => _context.SaveChangesAsync(cancellationToken);

    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
        _logger.LogInformation("UnitOfWork dispoused");
    }
}
