using sicf_DataBase.Repositories.Apelacion;
using sicf_DataBase.Repositories.Plantilla;
using sicf_Models.Constants;
using sicf_Models.Core;
using sicf_Models.Dto.Plantilla;
using sicfExceptions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static sicf_Models.Constants.Constants;

namespace sicf_BusinessHandlers.BusinessHandlers.Plantilla
{
    public class PlantillaService : IPlantillaService
    {
        private readonly IPlantillaRepository plantillaRepository;

        public PlantillaService(IPlantillaRepository plantillaRepository)
        { 
            this.plantillaRepository = plantillaRepository;
        }

        public async Task<PlantillaResponse> ObtenerSecciones(long idSolicitudServicio)
        {
            try
            {
                List<PlantillaResponseTree> jerarquia = new List<PlantillaResponseTree> ();
                List<PlantillaSPDTO> secciones = new List<PlantillaSPDTO>();

                secciones = await plantillaRepository.ObtenerSecciones(idSolicitudServicio);


                var validarMedidas = await plantillaRepository.ListadoMedidas(idSolicitudServicio);


                List<PlantillaInvolucradoDTO> involucrados = plantillaRepository.ObtenerInvolucrados(idSolicitudServicio);

                if (secciones.Count > 1 || (secciones.Count == 1 && secciones.First().idSolPSeccion != 0))
                {
                    List<PlantillaResponseTree> tree = plantillaRepository.ObtenerJerarquia(secciones, null);

                    PlantillaResponse response = new PlantillaResponse();

                    var seccion = secciones.First();

                    response.idSolPlantilla = seccion.idSolPlantilla;
                    response.nombrePlantilla = secciones.First().nombrePlantilla;
                    response.observacion = secciones.First().observacion;
                    response.tieneApelacion = seccion.tieneApelacion;
                    response.aprobado = seccion.aprobado;
                    response.apelacion = seccion.apelacion;
                    response.idAnexo = seccion.idAnexo;
                    response.aplicaRevision = secciones.First().aplicaRevision;
                    response.secciones = plantillaRepository.AsignarInvolucrados(secciones, involucrados);
                    response.tree = tree;
                    response.aplicaMedidas = validarMedidas.Item1;
                    response.medidasValidar = validarMedidas.Item2;
                    // meter reporte

                    
                    var seccionCambio = response.secciones.Where(s => s.idSeccionPlantilla == VictimaReporte.seccionReporteVictima).FirstOrDefault();

                     if(seccionCambio != null) {
                        var info = await plantillaRepository.InformacionVictimaReporte(idSolicitudServicio);
                        seccionCambio.textoSeccion = InterPolacionVictima(seccionCambio.textoSeccion! ,info );
                    }
                    
                    return response;
                }
                else
                {
                    throw new ControledException("No se encontraron secciones de un documento para la tarea o la tarea asignada no posee etiqueta de plantilla");
                }
            }
            catch (Exception ex)
            {
                throw new ControledException(ex.Message);
            }
        }

        public Task<bool> ActualizarSecciones(PlantillaGuardarDTO secciones) 
        {
            try
            {
                bool response = true;

                response = plantillaRepository.ActualizarSecciones(secciones).Result;

                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                throw new ControledException(ex.Message);
            }
        }

        public async Task<bool> FirmarPlantilla(PlantillaRequestFirmaDTO firma)
        {
            bool response = true;
            try
            {
                if(firma.cierre)
                    response = await plantillaRepository.AplicarMedidas(firma);
                
                long idSolicitud = await plantillaRepository.ActualizarPlantilla(firma);

                return response;
            }
            catch (Exception ex)
            {
                throw new ControledException(ex.Message);
            }
        }

        private string InterPolacionVictima(string texto, Tuple<string, string>? informacionVictima)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(texto);

                sb.Replace($"{VictimaReporte.interPolacionVictima[0]}", informacionVictima.Item1);
                sb.Replace($"{VictimaReporte.interPolacionVictima[1]}", informacionVictima.Item2);

                return sb.ToString();
            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        
        }  
    }
}
