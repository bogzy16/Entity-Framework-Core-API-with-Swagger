using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using REST_API.Models;

namespace REST_API.Repository
{
    public interface IDataRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllEmployee();
        Task<List<TEntity>> GetFilteredList(Request req);
        Task<TEntity> NewEmployee(Request req);
        Task<TEntity> UpdateEmployee(Request req);
        Task<TEntity> RemoveEmployee(Request req);
    }
}
