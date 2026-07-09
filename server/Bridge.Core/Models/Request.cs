using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.Core.Models
{
    public class Request:BaseModel
    {
        public User User { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Point? Location { get; set; }
        public RequestStatus Status { get; set; } = RequestStatus.Open;
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
