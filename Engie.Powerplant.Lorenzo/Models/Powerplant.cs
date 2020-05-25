using Engie.Powerplant.Lorenzo.Enums;

namespace Engie.Powerplant.Lorenzo.Models
{
    public class Powerplant
    {
        public string Name { get; set; }
        public PowerplantType Type { get; set; }
        public decimal Efficiency { get; set; }
        public int Pmin { get; set; }
        public int Pmax { get; set; }
        public int P { get; set; }
    }
}
