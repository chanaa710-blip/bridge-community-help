using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.Core.Repository
{
    public interface IRepository<T>
    {
        Task<T?> GetById(Guid id);
        Task<Guid> Add(T obj);
        Task<int> Update(T obj);
        Task<int> DeleteById(Guid id);
        Task<List<T>> GetAll();
    }
}
