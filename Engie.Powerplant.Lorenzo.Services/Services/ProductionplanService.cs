using Engie.Powerplant.Lorenzo.Business.Interfaces;
using Engie.Powerplant.Lorenzo.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Engie.Powerplant.Lorenzo.Business.Services
{
    public class ProductionplanService : IProductionplanService
    {
        public Task<IList<PowerplantModel>> CalculateUnitOfCommitment(IList<PowerplantModel> powerplants, int load, FuelsModel fuels)
        {
            throw new NotImplementedException();
        }
    }
}
