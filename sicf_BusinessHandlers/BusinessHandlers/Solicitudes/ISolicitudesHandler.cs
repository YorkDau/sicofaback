using sicf_Models.Dto.Ciudadano;
using sicf_Models.Dto.Solicitudes;
using sicf_Models.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_BusinessHandlers.BusinessHandlers.Solicitudes
{
    public interface ISolicitudesHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestSolicitudDto"></param>
        /// <returns></returns>
        public ResponseListaPaginada GetSolicitudes(RequestSolicitudDto requestSolicitudDto);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestConsultarCiudadano"></param>
        /// <returns></returns>
        public ResponseListaPaginada GetCiudadanos(RequestCiudadano requestConsultarCiudadano);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestSolicitudDto"></param>
        /// <returns></returns>
        public ResponseListaPaginada ValidarSolicitudes(RequestSolicitudDto requestSolicitudDto);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestCiudadano"></param>
        /// <returns></returns>
        public ResponseListaPaginada ValidarCiudadano(RequestCiudadano requestCiudadano);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestRegistrarCiudadano"></param>
        /// <returns></returns>
        public ResponseListaPaginada ValidarRegistrarCiudadano(RequestRegistrarCiudadano requestRegistrarCiudadano);

        /// Eliminado public ResponseCargaCiudadanoDto CargaCiudadanoSolicitud();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestRegistrarCiudadano"></param>
        /// <returns></returns>
        public ResponseListaPaginada RegistrarCiudadano(RequestRegistrarCiudadano requestRegistrarCiudadano);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestRegistrarCiudadano"></param>
        /// <returns></returns>
        public ResponseListaPaginada EditarCiudadano(RequestRegistrarCiudadano requestRegistrarCiudadano);

        /// <summary>
        /// Registro de involucrado
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Task<ResponseListaPaginada> RegistroInvolucrado(long id, List<RequestDatosInvolucrado> data);

        public ResponseListaPaginada ObtenerCiudadano(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numeroDocumento"></param>
        /// <returns></returns>
        public bool ConsultarNumeroDocumentoCiudadano(string numeroDocumento, int idtipoDocumento);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetNumeroSolicitud(long idComisaria);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestCrearSolicitud"></param>
        /// <returns></returns>
        public Task<long> CrearSolicitudCiudadano(RequestCrearSolicitud requestCrearSolicitud);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestActualizarSolicitud"></param>
        /// <returns></returns>
        public Task<long> ActualizarSolicitudCiudadano(RequestActualizarSolicitud requestActualizarSolicitud);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idCiudadano"></param>
        /// <returns></returns>
        public ResponseEditarCiudadano CargarDatosCiudadanoEditar(long idCiudadano);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        public ResponseListaPaginada ObtenerSolicitudServiciosCiudadano(int id, int idComisaria);
        public ResponseListaPaginada ObtenerSolicitudServiciosCiudadano(int idComisaria);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SolicitudServicioDetalleDTO ObtenerSolicitudServiciosCiudadanoDetalle(int id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SolicitudServicioDatosDTO ObtenerDatosSolicitud(int idSolicitud);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<ComisariaDTO> ComisariasPorMunicipio(int id);

        public List<ComisariaDTO> ObtenerComisariasTraslado(int idComisariaActual);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string RegistroRemisionSolicitud(RequestRemisionSolicitud data);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        List<EntidadExterna> ObtenerEntidadExterna();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_tipo_violencia"></param>
        /// <returns></returns>
        public ResponseListaPaginada ObtenerQuestionarioViolencia(int id_tipo_violencia);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tipo"></param>
        /// <returns></returns>
        public ResponseListaPaginada SexoOrientacionGenero(string tipo);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<TipoDiscapacidadDTO> ObtenerTipoDiscapacidad();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ResponseListaPaginada ObtenerNivelAcademico();


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ResponseListaPaginada ObtenerTIpoRelacion();


        /// <summary>
        /// 
        /// </summary>
        /// <param name="questinario"></param>
        /// <returns></returns>
        public ResponseListaPaginada RegistrarRespuestaQuestionario(RespuestaQuestionarioDTO questinario);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_ciudadano"></param>
        /// <returns></returns>
        public RequestDatosInvolucradoPrincipal1 ConsultaInvolucradoPrincipal(int id_ciudadano);

        public string ObtenerNumeroSolicitud(long idComisaria);

        public Task<SolicitudGeneralDTO> ConsultaGeneralSolicitud(long idSolicitudServicio);
        ResponseListaPaginada ConsultarSolicitudesGeneralesPorFiltros(ConsultaGeneralSolicitudRequestDTO solicitud);
        ResponseListaPaginada ConsultarPreSolicitudesGeneralesPorFiltros(ConsultaGeneralSolicitudRequestDTO solicitud);
        


        public List<RemisionSolicitudServicioComisariaAnteriorDTO> ObtenerRemisionSolicitudServicio(int idSolicitudServicio);
    }
}
