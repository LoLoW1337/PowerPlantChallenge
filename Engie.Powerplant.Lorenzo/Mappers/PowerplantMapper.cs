using Engie.Powerplant.Lorenzo.Business.Enums;
using Engie.Powerplant.Lorenzo.Business.Models;
using Engie.Powerplant.Lorenzo.Models;
using System;
using System.Linq.Expressions;

namespace Engie.Powerplant.Lorenzo.Mappers
{
    public class PowerplantMapper
    {
        internal static Expression<Func<Models.Powerplant, PowerplantModel>> MapPowerplantToDomain()
        {
            return powerplant => powerplant != null ? new PowerplantModel
            {
                Efficiency = powerplant.Efficiency,
                Name = powerplant.Name,
                Pmax = powerplant.Pmax,
                Pmin = powerplant.Pmin,
                Type = (PowerplantType)powerplant.Type
            } : null;
        }

        internal static Expression<Func<PowerplantModel, Response>> MapPowerplantToResponse()
        {
            return powerplant => powerplant != null ? new Response
            {
                Name = powerplant.Name,
                P = powerplant.P,
                CO2Cost = powerplant.CO2CostEmission,
                Cost = powerplant.RunningCost
            } : null;
        }
    }
}
