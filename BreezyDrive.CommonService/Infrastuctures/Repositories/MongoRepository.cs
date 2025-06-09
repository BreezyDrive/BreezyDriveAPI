using BreezyDrive.CommonService.Domain.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BreezyDrive.CommonService.Infrastuctures.Repositories
{
    public class MongoRepository<T> : IMongoRepository<T>
    {
        private readonly IMongoCollection<T> _collection;

        public MongoRepository(IMongoDatabase database, string collectionName)
        {
            _collection = database.GetCollection<T>(collectionName);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
            => await _collection.Find(Builders<T>.Filter.Empty).ToListAsync();

        public async Task<IEnumerable<T>> GetAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IFindFluent<T, T>, IFindFluent<T, T>> modify = null, 
            int? pageIndex = null, int? pageSize = null)
        {
            var query = _collection.Find(filter ?? (_ => true));

            if (modify != null)
            {
                query = modify(query);
            }

            if (pageIndex.HasValue && pageSize.HasValue)
            {
                query = query
                    .Skip((pageIndex.Value - 1) * pageSize.Value)
                    .Limit(pageSize.Value);
            }

            return await query.ToListAsync();
        }


        public async Task<T> GetByIdAsync(object id)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task InsertAsync(T entity)
            => await _collection.InsertOneAsync(entity);

        public async Task UpdateAsync(object id, T entity)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);
            await _collection.ReplaceOneAsync(filter, entity);
        }

        public async Task DeleteAsync(object id)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);
            await _collection.DeleteOneAsync(filter);
        }
    }
}