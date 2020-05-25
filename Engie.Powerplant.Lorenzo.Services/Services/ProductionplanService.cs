using Engie.Powerplant.Lorenzo.Business.Enums;
using Engie.Powerplant.Lorenzo.Business.Interfaces;
using Engie.Powerplant.Lorenzo.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Engie.Powerplant.Lorenzo.Business.Services
{
    public class ProductionplanService : IProductionplanService
    {
        private readonly IMeritOrderService meritOrderService;

        public ProductionplanService(IMeritOrderService meritOrderService)
        {
            this.meritOrderService = meritOrderService;
        }

        public async Task<IList<PowerplantModel>> CalculateUnitOfCommitment(IList<PowerplantModel> powerplants, int load, FuelsModel fuels)
        {
            var results = await meritOrderService.SetMeritOrder(powerplants, fuels);

            foreach (var r in results)
            {
                if (!r.IsUsed)
                    UsePowerplant(r, fuels, ref load);
                if (load == 0)
                    break;
            }
            return results;
        }

        private void UsePowerplant(PowerplantModel powerplant, FuelsModel fuels, ref int load)
        {
            
            powerplant.P = GetPowerGenerated(powerplant, fuels, load);
            UpdateExpectedLoadRemaining(ref load, powerplant.P);
            powerplant.IsUsed = true;

        }

        private void UpdateExpectedLoadRemaining(ref int load, int powerGenerated)
        {
            load -= powerGenerated;
        }

        private int GetPowerGenerated(PowerplantModel powerplant, FuelsModel fuels, int load)
        {
            switch (powerplant.Type)
            {
                case PowerplantType.Windturbine:
                    return (int)Math.Round(powerplant.Efficiency * powerplant.Pmax * (fuels.Wind / 100));
                case PowerplantType.Turbojet:
                    if (powerplant.Pmin <= load && load <= powerplant.Pmax)
                        return load;
                    else
                        return powerplant.Pmax;
                case PowerplantType.Gasfired:
                    if (powerplant.Pmin <= load && load <= powerplant.Pmax)
                        return load;
                    else
                        return powerplant.Pmax;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}