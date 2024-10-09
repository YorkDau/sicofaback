using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sicf_BusinessHandlers.BusinessHandlers.Filter;
using sicf_BusinessHandlers.Filters;
using System.Text.Json.Serialization;
using static sicf_Models.Constants.Constants;

namespace sicfServicesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PruebaFiltroController : ControllerBase
    {

        private IFilterService filterService;

        public PruebaFiltroController(IFilterService filterService)
        {
            this.filterService = filterService;
        }

        [HttpGet("test")]

        //[Filter(Parameter =("mater"))]
        //[Filter(Parameter = Politicas.politicaComisario)]

        //[TypeFilter(typeof(AuthenticationFilter),Arguments = new object[] { "value" } )]


        //[TypeFilter(typeof(AuthenticationFilter))]

        [MyAttribute("COM")]
        public IActionResult Caso() {

            return Ok("prueba");
        }
    }
}
