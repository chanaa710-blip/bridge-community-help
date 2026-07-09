using Bridge.Core.Models;
using Bridge.Core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.Core.Services
{
    public interface IAnswerService:IService<AnswerResource>
    {
        Task<IEnumerable<AnswerResource>> GetAnswersByRequestId(Guid requestId);
        Task<IEnumerable<AnswerResource>> GetAnswersByUserId(Guid userId);
    }
}
