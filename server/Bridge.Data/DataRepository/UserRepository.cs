using Bridge.Core.Models;
using Bridge.Core.Repository;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.Data.DataRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly BridgeDB _dbContext;
        public UserRepository(BridgeDB dbContext)
        { 
            _dbContext = dbContext;
        }
        public async Task<Guid> Add(User obj)
        {
            await _dbContext.Users.AddAsync(obj);
            await _dbContext.SaveChangesAsync();
            return obj.Id; 
        }

        public async Task<List<User>> GetAll()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<int> DeleteById(Guid id)
        {
           User user=await _dbContext.Users.FindAsync(id);
           if (user == null) return 0;
           _dbContext.Users.Remove(user);
           return await _dbContext.SaveChangesAsync();
        }

        public async Task<User?> GetByEmail(string email)
        {
            return await _dbContext.Users
                .FirstOrDefaultAsync(u=>u.Email==email);
        }

        public async Task<User?> GetById(Guid id)
        {
            return await _dbContext.Users.FindAsync(id);
        }

        public async Task<int> Update(User obj)
        {
            _dbContext.Users.Update(obj);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<List<User>> GetUsersInRadius(Point center, double radiusInMeters)
        {
            return await _dbContext.Users
                .Where(u => u.Location.Distance(center) <= radiusInMeters).ToListAsync();
        }

        public async Task<int> UpdateLocation(Guid userId, Point newLocation)
        {
            var user = new User() { Id = userId };
            _dbContext.Attach(user);
            user.Location = newLocation;
            _dbContext.Entry(user).Property(x => x.Location).IsModified = true;
            return await _dbContext.SaveChangesAsync(); 
        }
    }
}
