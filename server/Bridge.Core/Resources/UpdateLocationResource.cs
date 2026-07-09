using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.Core.Resources
{
    public class UpdateLocationResource
    {
            public Guid Id { get; set; }
            public double Lat { get; set; }
            public double Lng { get; set; }    
    }
}
