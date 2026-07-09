using Bridge.Core.Models;
using Bridge.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.Data.DataRepository
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly BridgeDB _dbContext;
        public AnswerRepository(BridgeDB dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> Add(Answer obj)
        {
            await _dbContext.Answers.AddAsync(obj);
            await _dbContext.SaveChangesAsync();
            return obj.Id; 
        }

        public async Task<int> DeleteById(Guid id)
        {
            Answer answer = await _dbContext.Answers.FindAsync(id);
            if (answer == null) return 0;
            _dbContext.Answers.Remove(answer);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Answer>> GetAll()
        {
            return await _dbContext.Answers.Include(a => a.Request)
                .Include(a => a.User).ToListAsync();
        }

        public async Task<List<Answer>> GetAnswersByRequestId(Guid requestId)
        {
            return await _dbContext.Answers
                .Where(a=>a.Request.Id == requestId).Include(a => a.Request)
                .Include(a => a.User).ToListAsync();
        }

        public async Task<Answer?> GetById(Guid id)
        {
            return await _dbContext.Answers.FindAsync(id);
        }

        public async Task<int> Update(Answer obj)
        {
            _dbContext.Answers.Update(obj);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Answer>> GetAnswersByUserId(Guid userId)
        {
            return await _dbContext.Answers
                .Where(a => a.UserId == userId)
                .OrderBy(a => a.CreatedAt)
                .ToListAsync();
        }
    }
}
