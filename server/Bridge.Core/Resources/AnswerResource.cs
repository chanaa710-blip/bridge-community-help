using Bridge.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.Core.Resources
{
    public class AnswerResource
    {
        public Guid Id { get; set; }
        public Guid? RequestId { get; set; }
        public string? RequestTitle { get; set; }
        public Guid? UserId { get; set; }
        public string? UserName { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
