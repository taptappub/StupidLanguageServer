using Domain.Repositories;

namespace DataAccess.EF;

public interface IRepositoryFactory
{
    IUserRepository User { get; }
    IGroupRepository Group { get; }
    IWordRepository Word { get; }
    ISentenceRepository Sentence { get; }
}
