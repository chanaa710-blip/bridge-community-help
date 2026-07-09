using Bridge.Core.Models;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.Core.Resources
{
    public class RequestResource
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public double? Lat { get; set; }
        public double? Lng { get; set; }
        public RequestStatus Status { get; set; }
        public Guid CategoryId { get; set; }
        public CategoryResource? Category { get; set; }
    }
}
