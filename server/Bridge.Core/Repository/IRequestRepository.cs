using Bridge.Core.Models;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.Core.Repository
{
    public interface IRequestRepository:IRepository<Request>
    {
        Task<int> UpdateStatus(Request request);
        Task<List<Request>> GetRequestsInRadius(Point center, double radiusInMeters);
        Task<List<Request>> GetRequestsByUserId(Guid userId);
    }
}
