using Engie.Powerplant.Lorenzo.Business.Interfaces;
using Engie.Powerplant.Lorenzo.Business.Models;

namespace Engie.Powerplant.Lorenzo.Business.Services
{
    public class RunningCostService : IRunningCostService
    {
        public void CalculateCO2EmissionCost(PowerplantModel powerplant, FuelsModel fuels)
        {
            if (powerplant.Type != Enums.PowerplantType.Windturbine)
                powerplant.CO2CostEmission = powerplant.P * 0.3m * fuels.Co2;
        }

        public void CalculateRunningCost(PowerplantModel powerplant, FuelsModel fuels)
        {
            if (powerplant.Type != Enums.PowerplantType.Windturbine)
                powerplant.RunningCost = powerplant.CO2CostEmission 
                    + (powerplant.P * ((powerplant.Type == Enums.PowerplantType.Gasfired) ? fuels.Gas : fuels.Kerosine));
        }
    }
}
