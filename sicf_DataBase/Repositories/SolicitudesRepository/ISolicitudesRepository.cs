using sicf_Models.Dto.Ciudadano;
using sicf_Models.Dto.Solicitudes;
using sicf_Models.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_DataBase.Repositories.SolicitudesRepository
{
    public interface ISolicitudesRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestSolicitudDto"></param>
        /// <returns></returns>
        public ResponseListaPaginada ObtenerSolicitudes(RequestSolicitudDto requestSolicitudDto);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestConsultarCiudadano"></param>
        /// <returns></returns>
        public ResponseListaPaginada ObtenerCiudadanos(RequestCiudadano requestCiudadano);

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


        // Eliminado public ResponseCargaCiudadanoDto CargaCiudadanoSolicitud();

        /// <summary>
        /// registro individual de victimas y agresores 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Tuple<bool?, int> RegistroInvolucrado( RequestDatosInvolucrado data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idSolicitudServicio"></param>
        /// <param name="idInvolucrado"></param>
        public void RegistroServicioInvolucrado(long idSolicitudServicio, long idInvolucrado);


        public ResponseListaPaginada ObtenerCiudadano(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numeroDocuemnto"></param>
        /// <returns></returns>
        public bool ConsultarNumeroDocumentoCiudadano(string numeroDocuemnto, int idtipoDocumento);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ObtenerNumeroSolicitud(long idComisaria);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestCrearSolicitud"></param>
        /// <returns></returns>
        public long CrearSolicitudCiudadano(RequestCrearSolicitud requestCrearSolicitud,string codSolicitud);

        /// <summary>
        /// /
        /// </summary>
        /// <param name="requestModificarSolicitud"></param>
        /// <returns></returns>
        public long ActualizarSolicitudCiudadano(RequestActualizarSolicitud requestModificarSolicitud);

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
        public ResponseListaPaginada ObtenerSolicitudServiciosCiudadano(int id, int idComisaria);
        public ResponseListaPaginada ObtenerSolicitudServiciosCiudadano(int idComisaria);

        List<ConsultaGeneralSolicitudDTO> ConsultaSolicitudGeneral(ConsultaGeneralSolicitudDTO solicitud);
        public ResponseListaPaginada ConsultarSolicitudesGeneralesPorFiltros(ConsultaGeneralSolicitudRequestDTO solicitud);
        public ResponseListaPaginada ConsultarPreSolicitudesGeneralesPorFiltros(ConsultaGeneralSolicitudRequestDTO solicitud);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SolicitudServicioDetalleDTO ObtenerSolicitudServiciosCiudadanoDetalle(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idSolicitud"></param>
        /// <returns></returns>
        public SolicitudServicioDatosDTO ObtenerDatosSolicitud(int idSolicitud);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<CantidadInvolucradosDTO> cantidadInvolucradosPorServicio(int id);

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
            public void RegistroRemisionSolicitud(RequestRemisionSolicitud data);

            /// <summary>
            /// 
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
            public int ConsultaRemisionExistentePorSolicitud(Int64 id);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<EntidadExterna> ObtenerEntidadExterna();


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_tipo_violencia"></param>
        /// <returns></returns>
        List<PreguntasTipoViolenciaDTO> ObtenerQuestionarioViolencia(int id_tipo_violencia);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tipo"></param>
        /// <returns></returns>
        public List<SexoGeneroOrientacionDTO> SexoOrientacionGenero(string tipo);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<TipoDiscapacidadDTO> ObtenerTipoDiscapacidad();


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<NivelAcademicoDTO> ObtenerNivelAcademico();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<TipoRelacionDTO> ObtenerTIpoRelacion();


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_tipo_violencia"></param>
        /// <returns></returns>
        public int ContadorPreguntasPorTipoViolencia(int id_tipo_violencia);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_solicitud_servicio"></param>
        /// <param name="data"></param>
        public void RegistroRespuestasQuestionario(Int64 id_solicitud_servicio, RespuestaPorPregunta data);

        public void RegistroInvolucradoPrincipalAproceso(Int64 idCiudadano, Int64 idSolicitudServicio);

        public RequestDatosInvolucradoPrincipal1 ConsultaInvolucradoPrincipal(int id_ciudadano);

        public int ValidarSolicitudPorCiudadano(long id_solicitud_servicio, long  @id_ciudadano);

        public Task<SolicitudGeneralDTO> ConsultaGeneralSolicitud(long idSolicitudServicio);


        public List<RemisionSolicitudServicioComisariaAnteriorDTO> ObtenerRemisionSolicitudServicio(int idSolicitudServicio);
    }
}
