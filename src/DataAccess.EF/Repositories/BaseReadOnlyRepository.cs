using Domain.Entities;
using Infrastructure.Authorization;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EF.Repositories;

class BaseReadOnlyRepository<TEntity> where TEntity : class
{
    private readonly DbContext _context;

    protected IQueryable<TEntity> Query => ApplyFilter(_context.Set<TEntity>());

    public BaseReadOnlyRepository(DbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    protected virtual IQueryable<TEntity> ApplyFilter(IQueryable<TEntity> query)
    {
        var userId = AuthorizationContext.CurrentUser?.Id;
        var entityInterfaces = typeof(TEntity).GetInterfaces();

        if (entityInterfaces.Contains(typeof(IOwnedEntity)))
        {
            query = query.Where(x => ((IOwnedEntity)x).User.Id == userId);
        }

        if (entityInterfaces.Contains(typeof(ISoftDelete)))
        {
            query = query.Where(x => ((ISoftDelete)x).IsDeleted == false);
        }

        return query;
    }
}
