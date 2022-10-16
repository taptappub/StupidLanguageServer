using Microsoft.Extensions.Logging;

namespace DataAccess.EF.Impl;

class UnitOfWorkFactory : IUnitOfWorkFactory
{
    private readonly string _connectionString;
    private readonly ILoggerFactory _loggerFactory;
    private readonly ILogger<UnitOfWorkFactory> _logger;

    public UnitOfWorkFactory(string connectionString, ILoggerFactory loggerFactory)
    {
        if (string.IsNullOrEmpty(connectionString)) throw new ArgumentException(nameof(connectionString));

        _connectionString = connectionString;
        _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
        _logger = loggerFactory.CreateLogger<UnitOfWorkFactory>();
    }

    public IUnitOfWork Create()
    {
        var uow = new UnitOfWork(_connectionString, _loggerFactory);
        _logger.LogInformation("UnitOfWork created");

        return uow;
    }

    public IReadOnlyUnitOfWork CreateReadOnly()
    {
        var uow = new ReadOnlyUnitOfWork(_connectionString, _loggerFactory);
        _logger.LogInformation("Readonly UnitOfWork created");

        return uow;
    }
}
