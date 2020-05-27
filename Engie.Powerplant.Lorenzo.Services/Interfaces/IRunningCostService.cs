using Engie.Powerplant.Lorenzo.Business.Models;
using System.Threading.Tasks;

namespace Engie.Powerplant.Lorenzo.Business.Interfaces
{
    public interface IRunningCostService
    {
        void CalculateCO2EmissionCost(PowerplantModel powerplant, FuelsModel fuels);
        void CalculateRunningCost(PowerplantModel powerplant, FuelsModel fuels);
    }
}
