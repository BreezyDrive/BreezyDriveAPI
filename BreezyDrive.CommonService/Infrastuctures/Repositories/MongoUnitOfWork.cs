using BreezyDrive.CommonService.Domain.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreezyDrive.CommonService.Infrastuctures.Repositories
{
    public class MongoUnitOfWork : IMongoUnitOfWork
    {
        private readonly IMongoDatabase _database;

        public MongoUnitOfWork(IMongoDatabase database)
        {
            _database = database;
        }

        public IMongoRepository<T> Repository<T>(string collectionName) where T : class
        {
            return new MongoRepository<T>(_database, collectionName);
        }
    }
}
