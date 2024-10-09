using sicf_Models.Dto.Ciudadano;
using sicf_Models.Dto.Presolicitud;
using sicf_Models.Dto.Solicitudes;
using sicf_Models.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_BusinessHandlers.BusinessHandlers.Presolicitud
{
    public interface IPresolicitudService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestPresolicitudDto"></param>
        /// <returns></returns>
        //public ResponseListaPaginada GetSolicitudes(RequestSolicitudDto requestSolicitudDto);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestRegistrarCiudadano"></param>
        /// <returns></returns>
        
        public string GetNumeroPresolicitud(long idComisaria);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestCrearSolicitud"></param>
        /// <returns></returns>
        public Task<long> CrearPresolicitud(PresolicitudOUT requestCrearPresolicitud, int idComisaria, int IdUsuarioSistema);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="numeroDocumento"></param>
        /// /// <param name="idTipoDocumento"></param>
        /// <returns></returns>

        public CiudadanoSolicitudes ConsultarCiudadanoTipoDocumentoDocumento(int idTipoDocumento, string numeroDocumento);

        /// <summary>
        /// 
        /// </summary>        
        /// /// <param name="idPresolicitud"></param>
        /// <returns></returns>
        public Task<PresolicitudDTO> ObtenerPresolicitud(long idPresolicitud);

        ///<summary>
        ///
        ///</summary>
        /// /// <param name="presolicitudABO"></param>
        /// <returns></returns>
        public Task<long> GuardarDecisionJuridica(PresolicitudABO presolicitudABO);

        /// <summary>
        /// 
        /// </summary>        
        /// /// <param name="idPresolicitud"></param>
        /// <returns></returns>
        public Task<PresolicitudInformeAbogado> ObtenerInformacionInformeAbogadoPresolicitud(long idPresolicitud);

        ///<summary>
        ///
        ///</summary>
        /// /// <param name="presolicitudABO"></param>
        /// <returns></returns>
        public Task<long> GuardarVerificacionDenuncia(PresolicitudVERDE presolicitudVERDE);

        ///<summary>
        ///
        ///</summary>
        /// /// <param name="presolicitudABO"></param>
        /// <returns></returns>
        public Task<bool> CerrarActuacionDenuncia(PresolicitudCEA presolicitudCEA, int idComisaria);

        public  Task CierrePresolicitudAsolicitud(CambioPreaSolicitudDTO data, int idComisaria);



    }

}
