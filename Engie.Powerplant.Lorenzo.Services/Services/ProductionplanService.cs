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
        public async Task<IList<PowerplantModel>> CalculateUnitOfCommitment(IList<PowerplantModel> powerplants, int load, FuelsModel fuels)
        {
            var results = PrioritizePowerplantUsage(powerplants, fuels);

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

        private IList<PowerplantModel> PrioritizePowerplantUsage(IList<PowerplantModel> powerplants, FuelsModel fuels)
        {
            int priority = 1;
            if (IsWindy(fuels))
            {
                SetPriority(powerplants, PowerplantType.Windturbine, priority);
                priority++;
            }
            if (fuels.Gas < fuels.Kerosine)
            {
                SetPriority(powerplants, PowerplantType.Gasfired, priority);
                priority++;
                SetPriority(powerplants, PowerplantType.Turbojet, priority);
                priority++;
            }
            else
            {
                SetPriority(powerplants, PowerplantType.Turbojet, priority);
                priority++;
                SetPriority(powerplants, PowerplantType.Gasfired, priority);
                priority++;
            }

            return powerplants.OrderBy(x => x.Priority).ThenByDescending(x => x.Efficiency).ToList();
        }

        private void SetPriority(IList<PowerplantModel> powerplants, PowerplantType powerplantType, int priority)
        {
            foreach (var p in powerplants.Where(x => x.Type == powerplantType).OrderByDescending(x => x.Efficiency))
            {
                p.Priority = priority;
            }
        }

        private bool IsWindy(FuelsModel fuels)
        {
            return fuels.Wind > 0;
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