using Bridge.Core.Models;
using Bridge.Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace Bridge.Data.DataRepository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BridgeDB _dbContext;
        public CategoryRepository(BridgeDB dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Category>> GetAll()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<Category?> GetById(Guid id)
        {
            return await _dbContext.Categories.FindAsync(id);
        }

        public async Task<List<Request>> GetRequestsByCategoryId(Guid categoryId)
        {
            return await _dbContext.Requests
                .Include(r => r.User)
                .Where(r => r.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<Category> GetByNameAsync(string name)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task<Guid> Add(Category obj)
        {
            await _dbContext.Categories.AddAsync(obj);
            await _dbContext.SaveChangesAsync();
            return obj.Id; 
        }

        public async Task<int> Update(Category obj) 
        { 
            _dbContext.Categories.Update(obj); 
            return await _dbContext.SaveChangesAsync(); 
        }
        public async Task<int> DeleteById(Guid id)
        {
            var cat = await _dbContext.Categories.FindAsync(id);
            if (cat == null) return 0;
            _dbContext.Categories.Remove(cat);
            return await _dbContext.SaveChangesAsync();
        }
    }
}