using Engie.Powerplant.Lorenzo.Business.Interfaces;
using Engie.Powerplant.Lorenzo.Business.Models;
using System;

namespace Engie.Powerplant.Lorenzo.Business.Services
{
    public class RunningCostService : IRunningCostService
    {
        public void CalculateCO2EmissionCost(PowerplantModel powerplant, FuelsModel fuels)
        {
            if (powerplant.Type != Enums.PowerplantType.Windturbine)
                powerplant.CO2CostEmission = Math.Round(powerplant.P * 0.3m * fuels.Co2, 2);
        }

        public void CalculateRunningCost(PowerplantModel powerplant, FuelsModel fuels)
        {
            if (powerplant.Type != Enums.PowerplantType.Windturbine)
                powerplant.RunningCost = Math.Round(powerplant.CO2CostEmission
                    + (
                        (powerplant.P / powerplant.Efficiency) 
                        * ((powerplant.Type == Enums.PowerplantType.Gasfired) ? fuels.Gas : fuels.Kerosine)
                    ), 2);
        }
    }
}
