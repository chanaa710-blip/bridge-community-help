using Bridge.Core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.Core.Services
{
    public interface IService<T>
    {
        Task<Guid> Add(T entity);
        Task<int> Update(T entity);
        Task<int> DeleteById(Guid id);
        Task<T?> GetById(Guid id);
        Task<IEnumerable<T>> GetAll();
    }
}
