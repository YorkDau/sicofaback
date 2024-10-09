using sicf_Models.Core;
using sicf_Models.Dto.Compartido;
using sicf_Models.Dto.Tarea;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_BusinessHandlers.BusinessHandlers.Tarea
{
    public interface ITareaHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idSolicitud"></param>
        /// <returns></returns>
        public Task<long> IniciarProceso(long idSolicitud, string codigoProceso);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="asignarTarea"></param>
        /// <returns></returns>
        public Task<long> AsignarTareaAsync(RequestAsignarTarea asignarTarea);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<IEnumerable<ResponseCasosPendienteAtencion>> GetCasosPendienteDeAtencionAsync(RequestCasosPendienteDeAtencion request);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public List<string> ValidarCasosPendienteDeAtencion(RequestCasosPendienteDeAtencion request);

        /// <summary>
        /// CerrarActuacion
        /// </summary>
        /// <param name="idTarea"></param>
        /// <returns></returns>
        public Task<bool> CerrarActuacionV2(long idTarea, string valorEtiqueta);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idTarea"></param>
        /// <returns></returns>
        public long CrearTareaProvicional(long idTarea);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<long> CrearEtiquetaAsync(EtiquetaDTO request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idTarea"></param>
        /// <returns></returns>
        public string? ObtenerEtiquetaSiguienteFlujo(long idTarea);

        public  Task<IEnumerable<TareaActividadDTO>> FlujoActualTareas(long idSolicitudServicio);

        public  Task CambiarFlujoTarea(CambioFlujoTareaDTO data);

        public  Task<long> UltimaTarea(long idSolicitudServicio);

        public SicofaActividad ConsultarActividad(long idTarea);

        public SicofaTarea ConsultarTarea(long idTarea);

    }
}
