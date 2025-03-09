using BreezyDrive.Common.Application.Utils.PaginatedList;
using BreezyDrive.Common.Domain.Interfaces;
using BreezyDrive.Common.Infrastuctures.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BreezyDrive.Common.Infrastuctures.Repositories
{
    public class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity>
        where TEntity : class
        where TContext : BaseDbContext<TContext>
    {
        internal TContext context;
        internal DbSet<TEntity> dbSet;

        public IQueryable<TEntity> Query => context.Set<TEntity>();

        public GenericRepository(TContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        // Query
        public bool Exists(Expression<Func<TEntity, bool>> predicate)
        {
            return dbSet.Any(predicate);
        }

        public IQueryable<TEntity> Entities => context.Set<TEntity>();

        // Non-Async Methods
        public IEnumerable<TEntity> GetAll()
        {
            return dbSet.AsEnumerable();
        }

        public IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "",
            int? pageIndex = null,
            int? pageSize = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (pageIndex.HasValue && pageSize.HasValue)
            {
                int validPageIndex = pageIndex.Value > 0 ? pageIndex.Value - 1 : 0;
                int validPageSize = pageSize.Value > 0 ? pageSize.Value : 10;

                query = query.Skip(validPageIndex * validPageSize).Take(validPageSize);
            }

            return query.ToList();
        }

        public TEntity GetById(object id)
        {
            return dbSet.Find(id);
        }

        public void Insert(TEntity entity)
        {
            dbSet.Add(entity);
            context.SaveChanges();
        }

        public void InsertRange(IEnumerable<TEntity> entities)
        {
            dbSet.AddRange(entities);
            context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(object id)
        {
            TEntity entity = dbSet.Find(id);
            if (entity != null)
            {
                Delete(entity);
            }
        }

        public void Delete(TEntity entity)
        {
            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
            context.SaveChanges();
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        // Async Methods
        public async Task<IList<TEntity>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(object id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<TEntity?> GetFirstOrDefaultAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "",
            int? pageIndex = null,
            int? pageSize = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (pageIndex.HasValue && pageSize.HasValue)
            {
                query = query.Skip((pageIndex.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }

            return await query.ToListAsync();
        }

        public async Task InsertAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(object id)
        {
            TEntity entity = await dbSet.FindAsync(id);
            if (entity != null)
            {
                dbSet.Remove(entity);
                await context.SaveChangesAsync();
            }
        }

        public async Task CommitAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task<IPaginatedList<TEntity>> GetPagingAsync(IQueryable<TEntity> query, int? pageIndex, int? pageSize)
        {
            if (query.Provider is IAsyncQueryProvider)
            {
                query = query.AsNoTracking();

                if (!pageIndex.HasValue || !pageSize.HasValue)
                {
                    var allItems = await query.ToListAsync();
                    return new PaginatedList<TEntity>(allItems, allItems.Count, 1, allItems.Count);
                }

                int count = await query.CountAsync();
                var paginatedItems = await query.Skip((pageIndex.Value - 1) * pageSize.Value)
                                                .Take(pageSize.Value)
                                                .ToListAsync();

                return new PaginatedList<TEntity>(paginatedItems, count, pageIndex.Value, pageSize.Value);
            }
            else
            {
                if (!pageIndex.HasValue || !pageSize.HasValue)
                {
                    var allItems = query.ToList();
                    return new PaginatedList<TEntity>(allItems, allItems.Count, 1, allItems.Count);
                }

                int count = query.Count();
                var paginatedItems = query.Skip((pageIndex.Value - 1) * pageSize.Value)
                                          .Take(pageSize.Value)
                                          .ToList();

                return new PaginatedList<TEntity>(paginatedItems, count, pageIndex.Value, pageSize.Value);
            }
        }
    }
}
