using Bridge.Core.Models;
using Bridge.Core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.Core.Services
{
    public interface IUserService:IService<UserResource>
    {
        Task<UserResource> Register(UserRegisterResource registerResource);
        Task<UserResource> LogIn(string email, string password);
        Task<IEnumerable<UserResource>> GetUsersInRadius(double lat, double lng, double radiusInMeters);
        Task<int> UpdateLocation(Guid userId, double lat, double lng);
        Task<UserResource?> UpdateProfile(UpdateUserResource updateResource);
    }
}
