using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreezyDrive.CommonService.Domain.Interfaces
{
    public interface IMongoUnitOfWork
    {
        IMongoRepository<T> Repository<T>(string collectionName) where T : class;
    }
}
