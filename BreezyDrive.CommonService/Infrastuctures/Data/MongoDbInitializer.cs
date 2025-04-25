using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreezyDrive.CommonService.Infrastuctures.Data
{
    public static class MongoDbInitializer
    {
        public static IMongoDatabase Initialize(string connectionString, string dbName)
        {
            var client = new MongoClient(connectionString);
            return client.GetDatabase(dbName);
        }
    }
}
