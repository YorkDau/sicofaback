using sicf_Models.Core;
using sicf_Models.Dto.Abogado;
using sicf_Models.Dto.Ciudadano;
using sicf_Models.Dto.Presolicitud;
using sicf_Models.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_DataBase.Repositories.PresolicitudesRepository
{
    public interface IPresolicitudRepository
    {
    
        public Task<CiudadanoSolicitudes> BuscarCiudadanoTipoDocumentoNumeroDocumento(int idTipoDocumento, string numeroDocumento);
        public bool ValidarTareaEstadoFirenteA(long? idSolicitud, string estado);
        public Task<bool> CrearPresolicitudServicioComplementaria(SicofaSolicitudServicioComplementaria sicofaSolicitudServicioComplementaria);
        public Task<long> CrearSolicitudServicio(SicofaSolicitudServicio sicofaSolicitudServicio, SicofaInvolucrado sicofaInvolucrado);       

        public Task<string> ObtenerNumeroPresolicitud(long idComisaria);
        public Task<PresolicitudDTO> ObtenerPresolicitud(long idPresolicitud);
        public Task<bool> ExisteSolicitudAnexo(long idSolicitudServicio, int idAnexo);

        public Task<bool> GuardarDecisionJuridicaSolicitud(PresolicitudABO presolicitudABO);

        public Task<bool> GuardarDecisionJuridicaComplementaria(PresolicitudABO presolicitudABO);
        
        public Task<bool> GuardarVerificacionDenunciaComplementaria(PresolicitudVERDE presolicitudVERDE);

        public Task<bool> CerrarActuacionDenuncia(PresolicitudCEA presolicitudCEA, long idComisaria);
        public Task<long> ObtenerSolicitudComplementario(long idSolicitudServicio);
        public Task<PresolicitudInformeAbogado> ConsultarInformacionInformeAbogadoPresolicitud(long idPresolicitud);
        public Task<bool> RegistrarTipoViolenciaSolicitud(PresolicitudVERDE presolicitudVERDE);
        public Task<int> ExisteTipoViolenciaSolicitud(PresolicitudTipoViolencia presolicitudTipoViolencia);


        public  Task<SicofaSolicitudServicioComplementaria> ComplementariaPorId(long idSolicitudServicio);

        public  Task ActualizaComplementoCita(long idSolicitud, long idCita);

        public  Task<long> CreacionSolicitud(long solicitudServicio, string codigoSolicitud);

        public  Task CambioEstadoPresolicitud(CambioPreaSolicitudDTO data, string subestado);

        public  Task<long> CambioTareaPresolAsolicitud(long idSolicitudPrevia, long idSolicitudNueva);

        public  Task CrearSolicitudComplentaria(long idSolicitudPrevio, long idsolicitudNuevo, bool cierre, string observacion);
    }
}
