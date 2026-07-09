using Bridge.Core.Models;
using Bridge.Core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.Core.Repository
{
    public interface IAnswerRepository:IRepository<Answer>
    {
        Task<List<Answer>> GetAnswersByRequestId(Guid requestId);
        Task<List<Answer>> GetAnswersByUserId(Guid userId);
    }
}
