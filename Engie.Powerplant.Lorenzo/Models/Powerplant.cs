using Engie.Powerplant.Lorenzo.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Engie.Powerplant.Lorenzo.Models
{
    public class Powerplant
    {
        public string Name { get; set; }
        public PowerplantType MyProperty { get; set; }
        public decimal Efficiency { get; set; }
        public int Pmin { get; set; }
        public int Pmax { get; set; }
        public int P { get; set; }
    }
}
