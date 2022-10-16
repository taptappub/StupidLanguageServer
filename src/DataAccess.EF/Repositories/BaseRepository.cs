using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Authorization;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EF.Repositories;

class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    private readonly DbContext _context;

    protected IQueryable<TEntity> Query => ApplyFilter(_context.Set<TEntity>());

    public BaseRepository(DbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public void Add(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
    }

    public void AddRange(IEnumerable<TEntity> entities)
    {
        _context.Set<TEntity>().AddRange(entities);
    }

    public void Remove(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
    }

    public void RemoveRange(IEnumerable<TEntity> entities)
    {
        _context.Set<TEntity>().RemoveRange(entities);
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
