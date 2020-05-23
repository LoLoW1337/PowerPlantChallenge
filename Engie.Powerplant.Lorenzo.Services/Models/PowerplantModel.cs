using Engie.Powerplant.Lorenzo.Business.Enums;

namespace Engie.Powerplant.Lorenzo.Business.Models
{
    public class PowerplantModel
    {
        public string Name { get; set; }
        public PowerplantType Type { get; set; }
        public decimal Efficiency { get; set; }
        public int Pmin { get; set; }
        public int Pmax { get; set; }
        public int P { get; set; }
        public bool IsUsed { get; set; }
        public int Priority { get; set; }
    }
}
