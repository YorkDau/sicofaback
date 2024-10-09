using CoreApiResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static sicf_Models.Constants.Constants;
using System.Net;
using sicf_Models.Dto.Seguimientos;

namespace sicfServicesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReporteController : BaseController
    {

        [HttpGet("Esquema/{reporte}/{fechaInicio}/{fechaFin}")]


        public IActionResult Esquema(string reporte, DateTime fechaInicio , DateTime fechaFin) 
        {
            try
            {
                List<InfoReporteSeguimientoDTO> salida = new List<InfoReporteSeguimientoDTO>();
                InfoReporteSeguimientoDTO data = new InfoReporteSeguimientoDTO();
                InfoReporteSeguimientoDTO data1 = new InfoReporteSeguimientoDTO();

                data.tipoDocuVictima = "cedula";
                data.edadVictima = 21;
                data.numeroTelVictima = "346578";
                data.nombreVictima = "Maria alejandra";
                data.numeroDocumentoVictima = "45678906";
                data.correoVictima = "mariaalejdnra@gmail.com";
                data.direccionVictima = "calle 123";
                data.ciudadRemision = "bogota";
                data.nombreComisario = "fernando lopez";
                data.lugarExpedicionVictima = "acacias";

                data1.tipoDocuVictima = "cedula";
                data1.edadVictima = 21;
                data1.numeroTelVictima = "346578";
                data1.nombreVictima = "mario";
                data1.numeroDocumentoVictima = "45678906";
                data1.correoVictima = "mario@gmail.com";
                data1.direccionVictima = "calle 123";
                data1.ciudadRemision = "bogota";
                data1.nombreComisario = "fernando lopez";
                data1.lugarExpedicionVictima = "acacias";

                salida.Add(data);
                salida.Add(data1);

                return CustomResult(Message.Ok,salida, HttpStatusCode.OK);

            }
            catch (Exception ex) { 
            
              


                    return CustomResult(Message.ErrorRequest, "No existen datos", HttpStatusCode.BadRequest);
            }
        
        }
    }
}
