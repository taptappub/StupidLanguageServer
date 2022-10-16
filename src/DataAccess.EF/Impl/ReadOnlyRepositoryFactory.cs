using DataAccess.EF.Repositories;
using DataAccess.EF.Repositories.Wrappers;
using Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace DataAccess.EF.Impl;

class ReadOnlyRepositoryFactory : IReadOnlyRepositoryFactory
{
    private readonly DatabaseContext _context;
    private readonly ILoggerFactory _loggerFactory;

    #region Fileds

    private IUserReadOnlyRepository? _userRepository;
    private IGroupReadOnlyRepository? _groupRepository;
    private IWordReadOnlyRepository? _wordRepository;
    private ISentenceReadOnlyRepository? _sentenceRepository;

    #endregion

    #region Properties

    public IUserReadOnlyRepository User => _userRepository ??=
        new UserReadOnlyRepositoryMetricsWrapper(
            new UserReadOnlyRepository(_context, _loggerFactory));
    public IGroupReadOnlyRepository Group => _groupRepository ??=
        new GroupReadOnlyRepositoryMetricsWrapper(
            new GroupReadOnlyRepository(_context, _loggerFactory));
    public IWordReadOnlyRepository Word => _wordRepository ??=
        new WordReadOnlyRepositoryMetricsWrapper(
            new WordReadOnlyRepository(_context, _loggerFactory));
    public ISentenceReadOnlyRepository Sentence => _sentenceRepository ??= 
        new SentenceReadOnlyRepositoryMetricsWrapper(
            new SentenceReadOnlyRepository(_context, _loggerFactory));

    #endregion

    public ReadOnlyRepositoryFactory(DatabaseContext context, ILoggerFactory loggerFactory)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
    }
}
