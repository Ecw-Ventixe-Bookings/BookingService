

using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

internal abstract class BaseRepository<TEntity>(SqlServerDbContext context)
    : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly SqlServerDbContext _context = context;
    protected readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public virtual async Task<Result> CreateAsync(TEntity entity)
    {
        try
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return new Result { Success = true };
        }
        catch (Exception e)
        {
            return new Result { Success = false, ErrorMessage = $"{nameof(TEntity)} Could not be created: --- {e.Message}" };
        }
    }   

    public virtual async Task<Result<IEnumerable<TEntity>>> GetAllAsync(Expression<Func<TEntity, bool>>? expression, Func<IQueryable<TEntity>, IQueryable<TEntity>>? includes = null)
    {
        try
        {
            if (includes is null)
            {
                var entities = await _dbSet.ToListAsync();
                return new Result<IEnumerable<TEntity>> { Success = true, Data = entities };
            }

            IQueryable<TEntity> query = _dbSet;
            query = includes(query);
            var entitiesIncluding = await query.ToListAsync();
            return new Result<IEnumerable<TEntity>> { Success = true, Data = entitiesIncluding };
        }
        catch (Exception e)
        {
            return new Result<IEnumerable<TEntity>>() { Success = false, ErrorMessage = $"Failed to get all entities: {e.Message}" };
        }
    }

    public virtual async Task<Result<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression, Func<IQueryable<TEntity>, IQueryable<TEntity>>? includes = null)
    {
        if (includes is null)
        {
            var entity = await _dbSet.FirstOrDefaultAsync(expression);
            return entity is null
                ? new Result<TEntity> { Success = false, ErrorMessage = $"{expression} does not exist." }
                : new Result<TEntity> { Success = true, Data = entity };
        }

        IQueryable<TEntity> query = _dbSet;
        query = includes(query);
        var entityIncluding = await query.FirstOrDefaultAsync(expression);
        return entityIncluding is null
            ? new Result<TEntity> { Success = false, ErrorMessage = $"{expression} does not exist." }
            : new Result<TEntity> { Success = true, Data = entityIncluding };
    }

    public virtual async Task<Result> UpdateAsync(TEntity entity)
    {
        try
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return new Result { Success = true };
        }
        catch (Exception e)
        {
            return new Result() { Success = false, ErrorMessage = $"{nameof(TEntity)} Could not be updated: {e.Message};" };
        }
    }

    public virtual async Task<Result> DeleteAsync(TEntity entity)
    {
        try
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return new Result { Success = true };
        }
        catch (Exception e)
        {
            return new Result() { Success = false, ErrorMessage = $"{nameof(TEntity)} Could not be deleted: {e.Message};" };
        }
    }
}
