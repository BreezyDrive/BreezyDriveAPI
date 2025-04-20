using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BreezyDrive.CommonService.Domain.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        // Queryable collection
        IQueryable<TEntity> Query { get; }

        // Non-async methods
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "",
            int? pageIndex = null,
            int? pageSize = null);

        TEntity GetById(object id);
        void Insert(TEntity entity);
        void InsertRange(IEnumerable<TEntity> entities);
        void Delete(object id);
        void Delete(TEntity entity);
        void Update(TEntity entity);
        bool Exists(Expression<Func<TEntity, bool>> predicate);
        void Commit();

        // Async methods
        Task<IList<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(object id);
        Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, string includeProperties = "");
        Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "",
            int? pageIndex = null,
            int? pageSize = null);

        Task InsertAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(object id);
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);

        Task CommitAsync();

        // Pagination support
        Task<IPaginatedList<TEntity>> GetPagingAsync(IQueryable<TEntity> query, int? pageIndex, int? pageSize);
    }
}
