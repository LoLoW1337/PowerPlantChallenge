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
        public async Task<IList<Models.Powerplant>> CalculateUnitOfCommitment(Payload payload)
        {
            return new List<Models.Powerplant>();
        }
    }
}
