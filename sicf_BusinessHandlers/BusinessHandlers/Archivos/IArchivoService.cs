using sicf_Models.Core;
using sicf_Models.Dto.Archivos;
using sicf_Models.Dto.Quorum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_BusinessHandlers.BusinessHandlers.Archivos
{
    public interface IArchivoService
    {

        public  Task<long> Carga(CargaArchivoDTO archivoDTO, long? idInvolucrado = null);

        public  Task Eliminar(EliminarArchivo eliminarArchivo);
        public  Task<string> ConsultaArchivo(string nombre,long idSolicitud);

          public Task<bool> CheckArchivo(string carpeta, string nombre);

        public Task<List<SolicitudDocumentoDTO>> ObtenerArchivos(ConsultaArchivo archivoDTO);

        public  Task<string> ObtenerArchivoPorId(long idsolicitudServicio, long idSolicitudAnexo);

        public  Task EditarArchivo(EditarArchivoDTO data);

        public  Task EliminarArchivoPorId(EliminarArchivoDTO data);

        public  Task CargaRemision(CargaArchivosRemisionDTO data);

        public  Task EditarRemision(EditarArchivoDTO data);

        public  Task ActualizarNotificacion(CargaArchivosRemisionDTO data);

        public Task CargarPruebaSolicitud(CargaPruebaSolicitudDTO data);

        public Task EliminarPruebaSolicitud(EliminarPruebaDTO data);

        public Task GuardarQuorum(RequestQuorumDTO quorum);

        public  Task EditarPruebaJuez(EditarPruebaJuez data);

        public  Task GuardarIncumplimiento(CargaArchivoIncumplimientoDTO data);

        public  Task EliminarIncumplimiento(long idAnexo);

        public Task<string> DescargarFormato(string nombreFormato, string codigo);

        public Task<List<SicofaFormatos>> ListaFormatos();

        public Task<long> CargaActaVerificacionDerechos(CargaActaVerificacionDerechosDTO data);

        public  Task GuardarNotificacion(CargaNotificacionPARD data);
    }
}
