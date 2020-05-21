using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Engie.Powerplant.Lorenzo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Engie.Powerplant.Lorenzo.Controllers
{
    [Route("api/productionplan")]
    [ApiController]
    public class ProductionplanController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Payload payload)
        {
            return new JsonResult(payload.Powerplants);
        }
    }
}