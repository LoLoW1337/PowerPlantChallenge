﻿using Engie.Powerplant.Lorenzo.Business.Enums;

namespace Engie.Powerplant.Lorenzo.Business.Models
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