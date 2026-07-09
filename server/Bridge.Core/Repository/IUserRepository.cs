using Bridge.Core.Models;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.Core.Repository
{
    public interface IUserRepository:IRepository<User>
    {
        Task<User?> GetByEmail(string email);
        Task<List<User>> GetUsersInRadius(Point center, double radiusInMeters);
        Task<int> UpdateLocation(Guid userId, Point newLocation);
    }
}
