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
                var sameTypePowerplants = results.Where(x => x.Type == r.Type && !r.IsUsed).ToList();
                if (!r.IsUsed)
                    UsePowerplant(r, fuels, ref load, sameTypePowerplants);
                if (load == 0)
                    break;
            }
            return results;
        }

        private void UsePowerplant(PowerplantModel powerplant, FuelsModel fuels, ref int load, IList<PowerplantModel> sameTypePowerplants)
        {

            powerplant.P = GetPowerGenerated(powerplant, fuels, load, sameTypePowerplants);
            UpdateExpectedLoadRemaining(ref load, powerplant.P);
            powerplant.IsUsed = true;

        }

        private void UpdateExpectedLoadRemaining(ref int load, int powerGenerated)
        {
            load -= powerGenerated;
        }

        private int GetPowerGenerated(PowerplantModel powerplant, FuelsModel fuels, int load, IList<PowerplantModel> sameTypePowerplants)
        {
            switch (powerplant.Type)
            {
                case PowerplantType.Windturbine:
                    return (int)Math.Round(powerplant.Efficiency * powerplant.Pmax * (fuels.Wind / 100));
                case PowerplantType.Turbojet:
                    return GenratePower(powerplant, load, sameTypePowerplants);
                case PowerplantType.Gasfired:
                    return GenratePower(powerplant, load, sameTypePowerplants);
                default:
                    throw new NotImplementedException();
            }
        }

        private int GenratePower(PowerplantModel powerplant, int load, IList<PowerplantModel> sameTypePowerplants)
        {
            if (load < powerplant.Pmin)
                return 0;
            if (load > powerplant.Pmax)
            {
                if (powerplant.Pmax + sameTypePowerplants.Sum(p => p.Pmin) <= load)
                    return powerplant.Pmax;
                else
                    return load / sameTypePowerplants.Count;
            }
            else if (powerplant.Pmin <= load && load <= powerplant.Pmax)
                return load;

            return 0;
        }
    }
}