using sicf_Models.Core;
using sicf_Models.Dto.Cita;
using sicf_Models.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_DataBase.Repositories.Cita
{
    public interface ICitaRepository
    {
        /// <summary>
        /// Consulta en BD y obtiene la lista de todos los Departamentos
        /// </summary>
        /// <returns></returns>
        public ResponseListaPaginada ObtenerDepertamentos();

        /// <summary>
        /// Consulta en BD y obtiene la lista de todas las CiudadesMunicipios
        /// </summary>
        /// <returns></returns>
        public ResponseListaPaginada ObtenerCiudadesMunicipios(long depID);



        /// <summary>
        /// 
        /// </summary>
        /// <param name="localidadID"></param>
        /// <returns></returns>
        public ResponseListaPaginada ObtenerComisarias(int ciudmunID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idComisaria"></param>
        /// <param name="idTipoAtencion"></param>
        /// <returns></returns>
        public List<DisponibilidadCitaDto> ObtenerDisponibilidadCitas(long idComisaria);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idComisaria"></param>
        /// <param name="idTipoAtencion"></param>
        /// <returns></returns>
        public List<CitaDto> ObtenerListaCitasDisponiblesProximosTresDias(long idComisaria);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestCitaDto"></param>
        /// <returns></returns>
        public ResponseListaPaginada CrearCita(RequestCitaDto requestCitaDto);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fecha"></param>
        /// <param name="citaDtos"></param>
        /// <returns></returns>
        public List<CitaHora> ObtenerHorasFecha(string fecha, List<CitaDto> citaDtos);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reservarCitaDto"></param>
        /// <returns></returns>
        public ResponseListaPaginada ReservarObtenerDisponibilidadCita(long idCita);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idCita"></param>
        /// <returns></returns>
        public ResponseListaPaginada AtenderCita(long idCiudadano);


        #region miguelCambios


        public  Task CambioEstadoCita(long idCita, int estado);

        public Task CrearCitaPresolicitud(RequestCitaDto requestCitaDto);
        #endregion miguelCambios

        public Task<ControledResponseDTO> GuardarCita(CrearCita data, int comisaria);

        public  Task ActualizarEstadoCita(long idCita, bool activo);

        public  Task<List<CitaDisponibleDTO>> ConsultarCita(int comisaria);


    }
}
