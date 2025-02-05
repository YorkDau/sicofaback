using sicf_Models.Core;
using sicf_Models.Dto.Cita;
using sicf_Models.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_BusinessHandlers.BusinessHandlers.Cita
{
    public interface ICitaHandler
    {
        /// <summary>
        /// Consulta y obtiene una lista de ResponseDepartamentoDto.
        /// </summary>
        /// <returns>ResponseListaPaginada.DatosPaginados = List<ResponseDepartamentoDto> </returns>
        public ResponseListaPaginada GetDepartamentos();

        /// <summary>
        /// Consulta y obtiene una lista de ResponseCiudadeMunicipioDto.
        /// </summary>
        /// <param name="depID"></param>
        /// <returns>ResponseListaPaginada.DatosPaginados = List<ResponseCiudadeMunicipioDto> </returns>
        public ResponseListaPaginada GetCiudadesMunicipios(int depID);

        /// <summary>
        /// Consulta y obtiene una lista de ResponseComisariaDto.
        /// </summary>
        /// <param name="localidadID"></param>
        /// <returns>ResponseListaPaginada.DatosPaginados = List<ResponseComisariaDto></returns>
        public ResponseListaPaginada GetComisarias(int ciudmunID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestCitaDto"></param>
        ResponseListaPaginada ValidarCita(RequestCitaDto requestCitaDto);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestCitaDto"></param>
        /// <returns></returns>
        public ResponseListaPaginada CrearCita(RequestCitaDto requestCitaDto);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reservarCitaDto"></param>
        /// <returns></returns>
        public ResponseListaPaginada ReservarObtenerDisponibilidadCita(long idCita);
        public ResponseListaPaginada ObtenerCitaComisaria(int id_comisaria);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reservarCitaDto"></param>
        /// <returns></returns>
        public ResponseListaPaginada AtenderCita(long idCiudadano);

        public Task<ControledResponseDTO> GuardarCita(CrearCita data, int comisaria);

        public Task<List<CitaDisponibleDTO>> ConsultarCita(int comisaria);
        public  Task ActualizarEstadoCita(long idCita, bool activo);
    }
}
