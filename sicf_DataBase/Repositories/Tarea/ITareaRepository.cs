using sicf_Models.Core;
using sicf_Models.Dto.Compartido;
using sicf_Models.Dto.Tarea;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_DataBase.Repositories.Tarea
{

    public interface ITareaRepository
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
        /// <param name="evento"></param>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public int ObtenerFlujoInicial(string evento, string codigo);

        public Task<bool> ActualizarComisariaTrasladoRemision(long idComisariaTraslado, long idSolitudServicio, int idUsuario);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="asignarTarea"></param>
        /// <returns></returns>
        public Task<long> AsignarTareaAsync(RequestAsignarTarea asignarTarea);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idSolicitudServicio"></param>
        /// <param name="idTarea"></param>
        /// <returns></returns>
        public long CrearEvaluacionPsicologica(long idSolicitudServicio, long idTarea);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<ResponseCasosPendienteAtencion>> ObtenerCasosPendienteAtencionAsync(RequestCasosPendienteDeAtencion request);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<ResponseCasosPendienteAtencion>> ObtenerCasosPendienteAtencionPerfil(RequestCasosPendienteDeAtencion request);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<ResponseCasosPendienteAtencion>> ObtenerCasosPendienteAtencionPerfilFiltro(RequestCasosPendienteDeAtencion request);
        /// <summary>
        /// 
        /// </summary>z
        /// <param name="tareaID"></param>
        /// <returns></returns>
        public int? ConsutarRiesgo(long tareaID);

        /// <summary>
        /// CerrarTarea
        /// </summary>
        /// <param name="idTarea"></param>
        /// <returns></returns>
        public bool CerrarTarea(long idTarea);

        /// <summary>
        /// CerrarActuacion
        /// </summary>
        /// <param name="idTarea"></param>
        /// <returns></returns>
        public Task<int> CerrarActuacion(long idTarea, string valorEtiqueta);

        /// <summary>
        /// CrearSiguienteTarea
        /// </summary>
        /// <param name="idTareaAnterior"></param>
        /// <param name="estadoTarea"></param>
        /// <returns></returns>
        public long CrearSiguienteTarea(SicofaTarea tareaAnterior);

        /// <summary>
        /// ObtenerListaFlujoSiguiente
        /// </summary>
        /// <param name="idTarea"></param>
        /// <returns></returns>
        /// //TODO: Eliminar cuando Flujo V2 funcione
        //public IEnumerable<SicofaFlujo?> ObtenerListaFlujoSiguiente(int idFlujo);


        /// <summary>
        /// ObtenerSiguienteFlujoCondicional
        /// </summary>
        /// <param name="idSolicitud"></param>
        /// <param name="idflujoactual"></param>
        /// <returns></returns>
        //TODO: Eliminar cuando Flujo V2 Funcione correctamente
        //public Task<bool?> ValidarSiguienteFlujo(SicofaFlujo flujo, string idSolicituServicio);

        /// <summary>
        /// ConsultarTarea
        /// </summary>
        /// <param name="idTarea"></param>
        /// <returns></returns>
        public SicofaTarea? ConsultarTarea(long idTarea);


        /// <summary>
        /// ObtenerIdFlujoActualTarea
        /// </summary>
        /// <param name="idTarea"></param>
        /// <returns></returns>
        public int? ObtenerIdFlujoActualTarea(long idTarea);

        /// <summary>
        /// ObtenerPerfilActividad
        /// </summary>
        /// <param name="idTarea"></param>
        /// <returns></returns>
        public IEnumerable<int> ObtenerPerfilActividad(long idFlujo);

        public long ProvisionalTarea(long idtarea);

        public long CrearEtiqueta(SicofaSolicitudEtiqueta etiqueta);

        public bool CrearEtiqueta(EtiquetaDTO etiquetaS);

        public string? ObtenerEtiquetaSiguienteFlujo(long idTarea);

        public  Task<IEnumerable<TareaActividadDTO>> FlujoActualTareas(long idSolicitudServicio);

        public  Task CambiarFlujoTarea(long idSolicitudServicio, long Idtarea, int actividad);

        public Task<long> UltimaTarea(long idTarea);

        public Task<long> TareaAnterior(long idTarea);

        public SicofaActividad ConsultarActividad(long idtarea);

    }
}