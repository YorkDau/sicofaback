using sicf_Models.Core;
using sicf_Models.Dto.Archivos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_DataBase.Repositories.Archivo
{
    public interface IArchivosRepository
    {

        public Task<bool> getSolicitudServicio(long idSolicitudServicio);

        public Task<Tuple<bool, bool, int>> ObtenerTipoDocumentoAnexo(string documento);

        public Task<long> RegistarArchivoSolicitud(int idDocumento, long idSolicitudServicio, string nombreDocumento, int idUsuario, long idTarea);

        public Task<List<SicofaSolicitudServicioAnexo>> ConsultarRegistroArchivo(long idSolicitudServicio, int idDocumento, long idTarea);

        public Task<string> ComisariaAsociada(long idSolicidServicio);
        public Task<bool> ActualizarComisariaTrasladoRemision(long idComisariaTraslado, long idSolitudServicio, int idUsuario);
        public Task<Tuple<bool, bool>> ValidarActualizacion(long idSolicitud, string tipoDocumento, long idTarea);

        public Task<string> ObtenerCodigoSolicitud(long idSolicitudServicio);

        public Task<int> ContadorArchivosMultiples(long idSolicitud, string tipoDocumento);

        public Task<SicofaSolicitudServicioAnexo> ObtenerArchivoPorId(long idSolicitudAnexo);

        public Task<string> ObtenerTipoAnexo(long idSolicitiudServicio, long idAnexo);

        public Task EliminarDocumentoServicio(long idDocumentoServicio);

        public Task EliminarDocumentoAnexo(long idSolicitudAnexo);

        public Task<long> ObtenerRegistroEliminancion(long idAnexo);

        public Task<SicofaFormatos> ObtenerFormato(string nombreFormato, string codigo);

        public Task<List<SicofaFormatos>> ListaFormatos();

        public Task<bool> ActualizarAnexoInvolucradoAdicional(CargaActaVerificacionDerechosDTO data);

        public Task GuardarNotificacion(CargaNotificacionPARD data, long idAnexo);

        public Task ActualizarNotificacionSolicitudAnexo(CargaNotificacionPARD data, long idAnexo);
    }
}
