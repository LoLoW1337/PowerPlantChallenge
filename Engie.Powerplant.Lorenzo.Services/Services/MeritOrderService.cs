using Engie.Powerplant.Lorenzo.Business.Enums;
using Engie.Powerplant.Lorenzo.Business.Interfaces;
using Engie.Powerplant.Lorenzo.Business.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Engie.Powerplant.Lorenzo.Business.Services
{
    public class MeritOrderService : IMeritOrderService
    {
        public async Task<IList<PowerplantModel>> SetMeritOrder(IList<PowerplantModel> powerplants, FuelsModel fuels)
        {
            int priority = 1;
            if (await IsWindy(fuels))
            {
                Set(powerplants, PowerplantType.Windturbine, ref priority);
            }
            if (fuels.Gas < fuels.Kerosine)
            {
                Set(powerplants, PowerplantType.Gasfired, ref priority);
                Set(powerplants, PowerplantType.Turbojet, ref priority);
            }
            else
            {
                Set(powerplants, PowerplantType.Turbojet, ref priority);
                Set(powerplants, PowerplantType.Gasfired, ref priority);
            }

            return powerplants.OrderBy(x => x.MeritOrder).ToList();
        }
        private async Task<bool> IsWindy(FuelsModel fuels)
        {
            return await Task.FromResult(fuels.Wind > 0);
        }

        private void Set(IList<PowerplantModel> powerplants, PowerplantType powerplantType, ref int priority)
        {
            foreach (var p in powerplants.Where(x => x.Type == powerplantType).OrderByDescending(x => x.Efficiency))
            {
                p.MeritOrder = priority;
                priority++;
            }
        }
    }


}
