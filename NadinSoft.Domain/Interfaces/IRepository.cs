using NadinSoft.Domain.Common;
using System.Linq.Expressions;

namespace NadinSoft.Domain.Interfaces
{
    public interface IRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        void Delete(TEntity entity);

        void DeleteRange(List<TEntity> entities);

        void Delete(TKey id);

        Task<List<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>>? filter = null,
            Expression<Func<TEntity, object>>? orderBy = null,
            Expression<Func<TEntity, object>>? orderByDesc = null,
            params string[] includeProperties);

        Task<TEntity?> GetByIdAsync(TKey id, params string[] includeProperties);

        Task<List<TEntity>> GetByIdsAsync(List<TKey> ids);

        Task InsertAsync(TEntity entity);

        Task InsertRangeAsync(List<TEntity> entities);

        Task SaveAsync();

        void Update(TEntity entityToUpdate);

        void UpdateRange(List<TEntity> entitiesToUpdate);

        Task<BasePaging<TEntity>> FilterAsync(
            FilterConditions<TEntity> filterConditions,
            int? page = 1,
            int? take = 12,
            Expression<Func<TEntity, object>>? orderBy = null,
            Expression<Func<TEntity, object>>? orderByDesc = null);

        Task<TEntity?> FirstOrDefaultAsync(
            Expression<Func<TEntity, bool>>? filter = null,
            Expression<Func<TEntity, object>>? orderBy = null,
            Expression<Func<TEntity, object>>? orderByDesc = null,
            params string[] includeProperties);

        Task<TEntity?> LastOrDefaultAsync(
            Expression<Func<TEntity, bool>>? filter = null,
            Expression<Func<TEntity, object>>? orderBy = null,
            Expression<Func<TEntity, object>>? orderByDesc = null,
            params string[] includeProperties);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter);
        
        Task<bool> SoftDelete(TKey id);
        Task<BasePaging<TEntity>> FilterAsync(FilterConditions<TEntity> filterConditions,
            FilterOrder<TEntity> orderBy,
            int? page = 1,
            int? take = 12);
    }
}
