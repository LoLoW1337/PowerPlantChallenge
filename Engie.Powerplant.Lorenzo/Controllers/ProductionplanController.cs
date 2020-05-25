using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Engie.Powerplant.Lorenzo.Business.Interfaces;
using Engie.Powerplant.Lorenzo.Business.Models;
using Engie.Powerplant.Lorenzo.Mappers;
using Engie.Powerplant.Lorenzo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Engie.Powerplant.Lorenzo.Controllers
{
    [ApiController]
    [Route("api/productionplan")]
    public class ProductionplanController : ControllerBase
    {
        private readonly IProductionplanService productionplanService;
        public ProductionplanController(IProductionplanService productionplanService)
        {
            this.productionplanService = productionplanService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Payload payload)
        {
            if (payload.Load == 0)
                return BadRequest("Load should be greater than 0");
            if (payload.Powerplants.Count == 0)
                return BadRequest("No powerplant have been received");

            var powerplants = new List<PowerplantModel>();
            foreach (var p in payload.Powerplants)
            {
                powerplants.Add(PowerplantMapper.MapPowerplantToDomain().Compile().Invoke(p));
            }

            var fuels = FuelsMapper.MapFuelsToDomain().Compile().Invoke(payload.Fuels);
            var result = await productionplanService.CalculateUnitOfCommitment(powerplants, payload.Load, fuels);

            var response = new List<Response>();
            foreach (var r in result)
            {
                response.Add(PowerplantMapper.MapPowerplantToResponse().Compile().Invoke(r));
            }

            return new JsonResult(response);
        }
    }
}