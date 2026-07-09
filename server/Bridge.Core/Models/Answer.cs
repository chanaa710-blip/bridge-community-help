using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.Core.Models
{
    public class Answer:BaseModel
    {
        public Request Request { get; set; }
        public Guid RequestId { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
        public string Content { get; set; }
    }
}
