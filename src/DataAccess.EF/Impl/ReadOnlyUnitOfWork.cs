using Microsoft.Extensions.Logging;

namespace DataAccess.EF.Impl;

class ReadOnlyUnitOfWork : IReadOnlyUnitOfWork
{
    private readonly DatabaseContext _context;
    private readonly ILogger<ReadOnlyUnitOfWork> _logger;

    public IReadOnlyRepositoryFactory Repositories { get; protected set;}

    public ReadOnlyUnitOfWork(string connectionString, ILoggerFactory loggerFactory)
    {
        if (string.IsNullOrEmpty(connectionString)) throw new ArgumentException(nameof(connectionString));
        if (loggerFactory is null) throw new ArgumentNullException(nameof(loggerFactory));

        _context = new DatabaseContext(connectionString);
        _logger = loggerFactory.CreateLogger<ReadOnlyUnitOfWork>();
        Repositories = new ReadOnlyRepositoryFactory(_context, loggerFactory);
    }

    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
        _logger.LogInformation("Readonly UnitOfWork dispoused");
    }
}
