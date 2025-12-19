using sicf_Models.Core;
using sicf_Models.Dto.Abogado;

namespace sicf_DataBase.Repositories.AbogadoRepository
{
    public interface IAbogadoRepository
    {
        public InvolucradosDTO ObtenerInvolucrados(long idSolicitudServicio);

        public Task RegistrarMedidaProteccion(RequestMedidaProteccionDTO data);

        public RequestMedidaProteccionDTO ObtenerInformacionMedidasProteccion(long idSolicitudServicio);

        public List<TestProcedure> Testprocedure();

        public Task<List<SicofaTipoRemision>> ObtenerTiposRemision();

        public Task<SicofaSolicitudServicio> ObtenerSolicitudServicio(long idSolicitud);

        public Task<Tuple<long, long>> getVictimaAgresor(long idSolicitudServicio);

        public Task<DocumentoRemisionDTO> OficioMedicinaLegal(long idVictima, long idSolicitudServicio);

        public Task<DocumentoRemisionDTO> SecretariaMujer(long idVictima, long idSolicitudServicio);

        public Task<DocumentoRemisionDTO> ProcesoPsicologiaExterna(long idVicitma, long IdAgresor, long idSolicitudServicio);

        public Task<DocumentoRemisionDTO> ApoyoPolicivoVictima(long idVictima, long Idagresor, long idSolicitudServicio);

        public Task<DocumentoRemisionDTO> DenunciaFiscalia(long idVictima, long Idagresor, long idSolicitudServicio);

        public Task<DocumentoRemisionDTO> VisitaDomiciaria(long idVictima, long idAgresor, long idsolicitudServicio);

        public Task<DocumentoRemisionDTO> RegimenSalud(long idVictima, long idAgresor, long idSolicitudServicio);

        public Task<DocumentoRemisionDTO> ProtocoloRiesgo(long idVictima, long idAgresor, long idSolicitudServicio);

        public Task<DocumentoRemisionDTO> HistoriaClinica(long idVictima, long idAgresor, long idSolicitudServicio);

        public Task<DocumentoRemisionDTO> RemisionFormatoPolicia(long idVictima, long idAgresor);

        public Task<DocumentoRemisionDTO> RemisionFormatoPersoneria(long idVictima, long IdAgresor, long idSolicitudServicio);

        public Task<DocumentoRemisionDTO> RemisionTratamientoTerapeutico(long idVictima, long idAgresor, long idSolicitudServicio);

        public Task<DocumentoRemisionDTO> SolicitudEvaluacionRiesgo(long idVictima, long idAgresor, long idSolicitudServicio);

        public Task<int> ObtenerRemision(string data);
        public Task<long> RegistrarSolicitudRemision(long idInvolucrado, int idRemision, long idSolicitudServicio, long? idAnexo);
        public Task<List<InvolucradoSelectDTO>> ObtenerListaInvolucrado(long idSolicitudServicio);
        public Task<List<RemisionDisponiblesDTO>> RemisionesDisponiblesPorInvolucrado(long idInvolucrado, string estado,long ? idSolicitud = null);

        public Task<List<RemisionesAsociada>> RemisionesAsociadasPorSolicitud(long idSolicitud, long idTarea);

        public Task ActualizarAnexoRemision(long idAnexo);
        public Task<string> LugarExpedicion(long idInvolucrado);



    }
}
