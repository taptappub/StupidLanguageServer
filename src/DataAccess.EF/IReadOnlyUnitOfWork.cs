namespace DataAccess.EF;

public interface IReadOnlyUnitOfWork : IAsyncDisposable
{
    IReadOnlyRepositoryFactory Repositories { get; }
}
