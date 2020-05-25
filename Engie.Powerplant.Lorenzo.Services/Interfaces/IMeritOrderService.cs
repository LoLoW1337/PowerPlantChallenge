using Engie.Powerplant.Lorenzo.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Engie.Powerplant.Lorenzo.Business.Interfaces
{
    public interface IMeritOrderService
    {
        Task<IList<PowerplantModel>> SetMeritOrder(IList<PowerplantModel> powerplants, FuelsModel fuels);
    }
}
