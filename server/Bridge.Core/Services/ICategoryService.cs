using Bridge.Core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.Core.Services
{
    public interface ICategoryService : IService<CategoryResource>
    {
        Task<CategoryResource> GetByNameAsync(string name);
        Task<IEnumerable<RequestResource>> GetRequestsByCategoryId(Guid categoryId);
    }
}