﻿using Engie.Powerplant.Lorenzo.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Engie.Powerplant.Lorenzo.Business.Interfaces
{
    public interface IProductionplanService
    {
        Task<IList<PowerplantModel>> CalculateUnitOfCommitment(IList<PowerplantModel> powerplants, int load, FuelsModel fuels);
    }
}
