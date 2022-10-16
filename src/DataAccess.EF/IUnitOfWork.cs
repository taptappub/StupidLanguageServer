namespace DataAccess.EF;

public interface IUnitOfWork : IAsyncDisposable 
{  
    IRepositoryFactory Repositories { get; }
    Task SaveChangesAsync (CancellationToken cancellationToken);
}
