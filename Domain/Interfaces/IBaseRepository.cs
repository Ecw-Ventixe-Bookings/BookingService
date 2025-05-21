
using Domain.Models;
using System.Linq.Expressions;

namespace Domain.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<Result> CreateAsync(TEntity entity);
    Task<Result<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression, Func<IQueryable<TEntity>, IQueryable<TEntity>>? includes = null);
    Task<Result<IEnumerable<TEntity>>> GetAllAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>>? includes = null);
    Task<Result> UpdateAsync(TEntity entity);
    Task<Result> DeleteAsync(TEntity entity);
}
