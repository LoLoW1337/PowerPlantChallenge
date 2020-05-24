using Engie.Powerplant.Lorenzo.Business.Models;
using Engie.Powerplant.Lorenzo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Engie.Powerplant.Lorenzo.Mappers
{
    public class FuelsMapper
    { 
        internal static Expression<Func<Fuels, FuelsModel>> MapFuelsToDomain()
        {
            return fuels => fuels != null ? new FuelsModel
            {
                Co2 = fuels.Co2,
                Gas = fuels.Gas,
                Kerosine = fuels.Kerosine,
                Wind = fuels.Wind
            } : null;
        }

        internal static Expression<Func<FuelsModel, Fuels>> MapFuelsToContract()
        {
            return fuels => fuels != null ? new Fuels
            {
                Co2 = fuels.Co2,
                Gas = fuels.Gas,
                Kerosine = fuels.Kerosine,
                Wind = (int)(fuels.Wind / 100)
            } : null;
        }
    }
}
