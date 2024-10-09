using sicf_Models.Core;
using sicf_Models.Dto;
using sicf_Models.Dto.EvaluacionPsicologica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static sicf_Models.Constants.Constants;

namespace sicf_DataBase.Repositories.EvaluacionPsicologica
{
    public interface IEvaluacionPsicologicaRepository 
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idSolicitudProceso"></param>
        /// <returns></returns>
        public AccionanteDTO IdentificarAccionante(long idSolicitudProceso);
/// <summary>
/// 
/// </summary>
/// <param name="tipoViolencia"></param>
/// <returns></returns>
        Task<List<QuestionarioRespuestaPreviaDTO>> ObtenerCuestionarioViolencia(int tipoViolencia, long idSolicitudServicio , long idTarea);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>



        public void RegistrarCuestionario(RespuestaCuestionarioDTO data, long idTarea);


        /// <summary>
        /// Cantidad de pregunas por tipo de violencia
        /// </summary>
        /// <param name="id"></param>
        /// <param name="respuesta"></param>
        /// <returns></returns>
     

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DatosInstitucionesDTO IdentificarDatosInstitucionales(long id);

        InformacionVictimaDTO ObtenerInvolucrado(long id, bool esvictima, bool principal);

        public void ActualizarInvolucrado(ActualizacionInvolucradoDTO data);

        public Task<EvaluacionRiegoDTO> EvaluacionRiesgosPorSolicitud(long idSolicitud, long? Idtarea);

        public DescripcionHechosDTO ObtenerDescripcionHechosPorSolicitud(long id);

        public void ActualizarDescripcioHechosPorSolicitud(DescripcionHechosDTO data);

        public long CrearEvaluacionPsicologica(long idSolicitudServicio, long? idTarea);

        public SicofaInvolucrado ConsultarInvolucrado(long id);
        
        public SicofaEvaluacionPsicologica ObtenerEvaluacionPsicologica(long idSolicitud, long? IdTarea);

        public void RegistrarRecomendacion(long idSolicitudServicio, string descripcion, long? idTarea);

        public List<EvaluacionOrientacionRespuesta> EvaluacionOrientacion(long idSolicitudServicio, string data, long? idTarea);

        public void RegistrarEvaluacionOrientacion(RegistroEvaluacionEmocionalDTO data, long idTarea);

        public  Task ActualizarEvaluacionPsicologica(RegistroEvaluacionEmocionalDTO data, long idTarea);

        public ObtenerEvaluacionPsicologicaEmocionalDTO ObtenerEvaluacionPsicologicaEmocional(long idSolicituServicio, string tipoDominio, long idTarea);

        public VictimaPrincipalDTO ObtenerVictimaPrincipal(long idSolicitud);

        public void ActualizaEvaluacionPsicologicaEntrevista(EvaluacionPsicologicaEntrevistaDTO data, long idTarea);

        public EvaluacionPsicologicaEntrevistaDTO ObtenerEvaluacionPsicologicaEntrevista(long idSolicitud, long idTarea);

        public InvolucradosReporte13DTO ReporteHU13(long idSolicitudServicio);


        //public List<EvaluacionPsicologicaReporte17DTO> ObtenerEvaluacionPsicologicaReporte17(long idSolicitud, string tipoDoc);

    }
}
