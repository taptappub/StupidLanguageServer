using Microsoft.Extensions.Logging;

namespace DataAccess.EF;

public interface IUnitOfWorkFactory
{
    IUnitOfWork Create();

    IReadOnlyUnitOfWork CreateReadOnly();
}
