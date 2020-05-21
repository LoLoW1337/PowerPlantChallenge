using System.Collections.Generic;

namespace Engie.Powerplant.Lorenzo.Business.Models
{
    public class Payload
    {
        public int Load { get; set; }
        public Fuels Fuels { get; set; }
        public IList<Powerplant> Powerplants { get; set; }
    }
}
