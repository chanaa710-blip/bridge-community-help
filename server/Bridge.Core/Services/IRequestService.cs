using Bridge.Core.Models;
using Bridge.Core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.Core.Services
{
    public interface IRequestService:IService<RequestResource>
    {
        Task<IEnumerable<RequestResource>> GetRequestsByLocationAndCategory(double lat, double lng, double radiusInMeters, Guid? categoryId);
        Task<bool> UpdateStatus(Guid requestId, RequestStatus newStatus);
        Task<IEnumerable<RequestResource>> GetRequestsByUserId(Guid userId);
    }
}
