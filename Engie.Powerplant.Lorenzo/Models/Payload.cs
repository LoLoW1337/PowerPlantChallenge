using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Engie.Powerplant.Lorenzo.Models
{
    public class Payload
    {
        public int Load { get; set; }
        public Fuels Fuels { get; set; }
        public IList<Powerplant> Powerplants { get; set; }
    }
}
