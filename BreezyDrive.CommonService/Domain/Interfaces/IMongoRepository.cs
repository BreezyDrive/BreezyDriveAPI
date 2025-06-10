using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace BreezyDrive.CommonService.Domain.Interfaces
{
    public interface IMongoRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAsync(
            Expression<Func<T, bool>>? filter = null,
            Func<IFindFluent<T, T>, IFindFluent<T, T>>? modify = null,
            int? pageIndex = null,
            int? pageSize = null);

        Task<T> GetByIdAsync(object id);
        Task InsertAsync(T entity);
        Task UpdateAsync(object id, T entity);
        Task DeleteAsync(object id);
    }

}
