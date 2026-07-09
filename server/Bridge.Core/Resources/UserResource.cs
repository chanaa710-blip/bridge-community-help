using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.Core.Resources
{
    public class UserResource
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; }
        public string Phone { get; set; }
        public double? Lat { get; set; }
        public double? Lng { get; set; }
    }
}
