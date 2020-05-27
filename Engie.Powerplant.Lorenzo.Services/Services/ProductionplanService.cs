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
        private readonly IRunningCostService runningCostService;

        public ProductionplanService(IMeritOrderService meritOrderService, IRunningCostService runningCostService)
        {
            this.meritOrderService = meritOrderService;
            this.runningCostService = runningCostService;
        }

        public async Task<IList<PowerplantModel>> CalculateUnitOfCommitment(IList<PowerplantModel> powerplants, int load, FuelsModel fuels)
        {
            var results = await meritOrderService.SetMeritOrder(powerplants, fuels);

            foreach (var r in results)
            {
                var nextTypePowerplant = results.Where(x => x.MeritOrder > r.MeritOrder && !r.IsUsed).FirstOrDefault();
                if (!r.IsUsed)
                    UsePowerplant(r, fuels, ref load, nextTypePowerplant);
                if (load == 0)
                    break;
            }

            return results;
        }

        private void UsePowerplant(PowerplantModel powerplant, FuelsModel fuels, ref int load, PowerplantModel nextTypePowerplant)
        {

            powerplant.P = GetPowerGenerated(powerplant, fuels, load, nextTypePowerplant);
            UpdateExpectedLoadRemaining(ref load, powerplant.P);
            runningCostService.CalculateCO2EmissionCost(powerplant, fuels);
            runningCostService.CalculateRunningCost(powerplant, fuels);
            powerplant.IsUsed = true;

        }

        private void UpdateExpectedLoadRemaining(ref int load, int powerGenerated)
        {
            load -= powerGenerated;
        }

        private int GetPowerGenerated(PowerplantModel powerplant, FuelsModel fuels, int load, PowerplantModel nextTypePowerplant = null)
        {
            if (powerplant.Type == PowerplantType.Windturbine)
                return UseWindturbine(powerplant, fuels);
            else
                return UseTurboJetAndGasFired(powerplant, load, nextTypePowerplant);

        }

        private int UseWindturbine(PowerplantModel powerplant, FuelsModel fuels)
        {
            return (int)Math.Round(powerplant.Efficiency * powerplant.Pmax * (fuels.Wind / 100));
        }

        private int UseTurboJetAndGasFired(PowerplantModel powerplant, int load, PowerplantModel nextTypePowerplant = null)
        {
            if (load < powerplant.Pmin)
                return 0;
            if (load > powerplant.Pmax)
            {
                if (nextTypePowerplant != null && powerplant.Pmax + nextTypePowerplant.Pmin <= load)
                    return load / 2;
                else
                    return powerplant.Pmax;
            }
            else if (powerplant.Pmin <= load && load <= powerplant.Pmax)
                return load;

            return 0;
        }
    }
}