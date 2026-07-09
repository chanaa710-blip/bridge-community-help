using Bridge.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bridge.Core.Repository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category> GetByNameAsync(string name);
        Task<List<Request>> GetRequestsByCategoryId(Guid categoryId);
    }
}