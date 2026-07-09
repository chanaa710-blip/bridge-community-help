using Bridge.Core.Models;
using Bridge.Core.Repository;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.Data.DataRepository
{
    public class RequestRepository : IRequestRepository
    {
        private readonly BridgeDB _dbContext;
        public RequestRepository(BridgeDB dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Guid> Add(Request obj)
        {
            await _dbContext.Requests.AddAsync(obj);
            await _dbContext.SaveChangesAsync();
            return obj.Id;
        }

        public async Task<List<Request>> GetAll()
        {
            return await _dbContext.Requests
                .Include(a => a.User)
                .Include(a => a.Category)
                .ToListAsync();
        }

        public async Task<int> DeleteById(Guid id)
        {
            Request request = await _dbContext.Requests.FindAsync(id);
            if (request == null) return 0;
            _dbContext.Requests.Remove(request);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<Request?> GetById(Guid id)
        {
            return await _dbContext.Requests
                .Include(r => r.User)
                .Include(r => r.Category)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<int> Update(Request obj)
        {
            _dbContext.Requests.Update(obj);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Request>> GetRequestsInRadius(Point center, double radiusInMeters)
        {
            return await _dbContext.Requests
                .Include(r => r.User)
                .Include(r => r.Category)
                .Where(r => (r.Status == RequestStatus.Open || r.Status == RequestStatus.InProgress) &&
                       r.Location.Distance(center) <= radiusInMeters)
                .ToListAsync();
        }

        public async Task<List<Request>> GetRequestsByUserId(Guid userId)
        {
            return await _dbContext.Requests
                .Where(r => r.UserId == userId)
                .Include(r => r.User)
                .Include(r => r.Category)
                .ToListAsync();
        }

        public async Task<int> UpdateStatus(Request request)
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}