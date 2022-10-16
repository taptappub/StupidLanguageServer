using Domain.Repositories;

namespace DataAccess.EF;

public interface IReadOnlyRepositoryFactory
{
    IUserReadOnlyRepository User { get; }
    IGroupReadOnlyRepository Group { get; }
    IWordReadOnlyRepository Word { get; }
    ISentenceReadOnlyRepository Sentence { get; }
}
