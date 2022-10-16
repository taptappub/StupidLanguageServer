using DataAccess.EF.Repositories;
using DataAccess.EF.Repositories.Wrappers;
using Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace DataAccess.EF.Impl;

class RepositoryFactory : IRepositoryFactory
{
    private readonly DatabaseContext _context;
    private readonly ILoggerFactory _loggerFactory;

    #region Fileds

    private IUserRepository? _userRepository;
    private IGroupRepository? _groupRepository;
    private IWordRepository? _wordRepository;
    private ISentenceRepository? _sentenceRepository;

    #endregion

    #region Properties

    public IUserRepository User => _userRepository ??=
        new UserRepositoryMetricsWrapper(
            new UserRepository(_context, _loggerFactory));
    public IGroupRepository Group => _groupRepository ??=
        new GroupRepositoryMetricsWrapper(
            new GroupRepository(_context, _loggerFactory));
    public IWordRepository Word => _wordRepository ??=
        new WordRepositoryMetricsWrapper(
            new WordRepository(_context, _loggerFactory));
    public ISentenceRepository Sentence => _sentenceRepository ??=
        new SentenceRepositoryMetricsWrapper(
            new SentenceRepository(_context, _loggerFactory));

    #endregion

    public RepositoryFactory(DatabaseContext context, ILoggerFactory loggerFactory)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
    }





}
