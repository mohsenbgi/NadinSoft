using Microsoft.EntityFrameworkCore;
using NadinSoft.Domain.Common;
using NadinSoft.Domain.Interfaces;
using System.Linq.Expressions;

namespace NadinSoft.Infra.Data.Repositories
{
    public class EfRepository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        #region Fields

        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        #endregion

        #region Constructor

        public EfRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        #endregion

        #region Methods

        public virtual async Task<List<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>>? filter = null,
            Expression<Func<TEntity, object>>? orderBy = null,
            Expression<Func<TEntity, object>>? orderByDesc = null,
            params string[] includeProperties)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            if (orderBy is not null)
            {
                query = query.OrderBy(orderBy);
            }

            if (orderByDesc is not null)
            {
                query = query.OrderByDescending(orderByDesc);
            }

            return await query.ToListAsync();
        }

        public virtual Task<List<TEntity>> GetByIdsAsync(List<TKey> ids)
        {
            return _dbSet.Where(e => ids.Contains(e.Id))
                .ToListAsync();
        }

        public virtual Task<BasePaging<TEntity>> FilterAsync(
            FilterConditions<TEntity> filterConditions,
            FilterOrder<TEntity> orderBy,
            int? page = 1,
            int? take = 12)
        {
            var orderByAsc = orderBy.IsAscending ? orderBy.OrderBy : null;
            var orderByDesc = !orderBy.IsAscending ? orderBy.OrderBy : null;

            return FilterAsync(filterConditions, page, take, orderByAsc, orderByDesc);
        }


        public virtual async Task<BasePaging<TEntity>> FilterAsync(
            FilterConditions<TEntity> filterConditions,
            int? page = 1,
            int? take = 12,
            Expression<Func<TEntity, object>>? orderBy = null,
            Expression<Func<TEntity, object>>? orderByDesc = null)
        {
            IQueryable<TEntity> query = _dbSet;

            foreach (var filterCondition in filterConditions)
            {
                query = query.Where(filterCondition);
            }

            if (orderBy is not null)
            {
                query = query.OrderBy(orderBy);
            }
            else if (orderByDesc is not null)
            {
                query = query.OrderByDescending(orderByDesc);
            }
            else
            {
                query = query.OrderByDescending(entity => entity.CreatedDateOnUtc);
            }

            var filter = new BasePaging<TEntity>();
            filter.TakeEntity = take ?? 12;
            filter.Page = page ?? 1;

            return filter.Paging(query);
        }

        public virtual async Task<TEntity?> GetByIdAsync(TKey id, params string[] includeProperties)
        {
            if (id is null) return null;

            var query = _dbSet.AsQueryable();

            foreach (var inlcudeProperty in includeProperties)
            {
                query = query.Include(inlcudeProperty);
            }

            return await query.FirstOrDefaultAsync(entity => entity.Id.Equals(id));
        }

        public virtual async Task<TEntity?> FirstOrDefaultAsync(
            Expression<Func<TEntity, bool>>? filter = null,
            Expression<Func<TEntity, object>>? orderBy = null,
            Expression<Func<TEntity, object>>? orderByDesc = null,
            params string[] includeProperties)
        {
            var query = _dbSet.AsQueryable();

            if (filter is not null)
            {
                query = query.Where(filter);
            }

            foreach (var inlcudeProperty in includeProperties)
            {
                query = query.Include(inlcudeProperty);
            }

            if (orderBy is not null)
            {
                query = query.OrderBy(orderBy);
            }

            if (orderByDesc is not null)
            {
                query = query.OrderByDescending(orderByDesc);
            }

            return await query.FirstOrDefaultAsync();
        }

        public virtual async Task<TEntity?> LastOrDefaultAsync(
            Expression<Func<TEntity, bool>>? filter = null,
            Expression<Func<TEntity, object>>? orderBy = null,
            Expression<Func<TEntity, object>>? orderByDesc = null,
            params string[] includeProperties)
        {
            var query = _dbSet.AsQueryable();

            if (filter is not null)
            {
                query = query.Where(filter);
            }

            foreach (var inlcudeProperty in includeProperties)
            {
                query = query.Include(inlcudeProperty);
            }

            if (orderBy is not null)
            {
                query = query.OrderBy(orderBy);
            }

            if (orderByDesc is not null)
            {
                query = query.OrderByDescending(orderByDesc);
            }

            return await query.LastOrDefaultAsync();
        }

        public virtual async Task InsertAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual async Task InsertRangeAsync(List<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public virtual void Delete(TKey id)
        {
            TEntity entityToDelete = _dbSet.Find(id);
            if (entityToDelete != null)
            {
                Delete(entityToDelete);
            }
        }

        public virtual void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual void DeleteRange(List<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);

        }

        public virtual void Update(TEntity entityToUpdate)
        {
            _dbSet.Update(entityToUpdate);
        }

        public virtual void UpdateRange(List<TEntity> entitiesToUpdatee)
        {
            _dbSet.UpdateRange(entitiesToUpdatee);
        }

        public virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter)
        {
            return _dbSet.AnyAsync(filter);
        }

        public virtual async Task<bool> SoftDelete(TKey id)
        {
            var result = 0;

            result = await _dbSet
                .Where(e => e.Id.Equals(id))
                .ExecuteUpdateAsync(s => s.SetProperty(e => e.IsDeleted, true)
                                          .SetProperty(e => (e as BaseEntity<TKey>).UpdatedDateOnUtc, DateTime.UtcNow));

            return result > 0;
        }

        public virtual async Task SaveAsync() => await _context.SaveChangesAsync();

        #endregion
    }
}
