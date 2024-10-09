using sicf_Models.Core;
using sicf_Models.Dto;
using sicf_Models.Dto.EvaluacionPsicologica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_BusinessHandlers.BusinessHandlers.EvaluacionPsicologica
{
    public interface IEvaluacionPsicologicaService
    {

        public AccionanteDTO IdentificarAccionante(long idSolicitudProceso);

        public DatosInstitucionesDTO IdentificarDatosInstitucionales(long id);
        Task<List<QuestionarioRespuestaPreviaDTO>> ObtenerCuestionarioViolencia(int tipoViolencia, long idSolicitudServicio, long? idTarea);

        public void RegistrarCuestionario(RespuestaCuestionarioDTO data);

        public InformacionVictimaDTO ObtenerInvolucrado(long id, bool esvictima, bool principal);

        public void ActualizarInvolucrado(ActualizacionInvolucradoDTO data);

        public Task<EvaluacionRiegoDTO> EvaluacionRiesgosPorSolicitud(long idSolicitud);

        public DescripcionHechosDTO ObtenerDescripcionHechosPorSolicitud(long id);

        public void ActualizarDescripcioHechosPorSolicitud(DescripcionHechosDTO data);

        public long CrearEvaluacionPsicologica(long idSolicitudServicio, long idTarea);

        public void RegistrarRecomendacion(long idSolicitudServicio, string descripcion);

        public List<EvaluacionOrientacionRespuesta> EvaluacionOrientacion(long idSolicitudServicio, string data);

        public Task RegistrarEvaluacionOrientacion(RegistroEvaluacionEmocionalDTO data);

        public Task ActualizarEvaluacionPsicologica(RegistroEvaluacionEmocionalDTO data);

        public Task<ObtenerEvaluacionPsicologicaEmocionalDTO>  ObtenerEvaluacionPsicologicaEmocional(long idSolicitudServicio, string tipoDominio);

        public VictimaPrincipalDTO ObtenerVictimaPrincipal(long idSolicitud);

        public  void ActualizaEvaluacionPsicologicaEntrevista(EvaluacionPsicologicaEntrevistaDTO data);

        public  EvaluacionPsicologicaEntrevistaDTO ObtenerEvaluacionPsicologicaEntrevista(long idSolicitud );

        public  InvolucradosReporte13DTO Reporte13(long idSolicitudServicio);

        //public List<EvaluacionPsicologicaReporte17DTO> ObtenerEvaluacionPsicologicaReporte17(long idSolicitud, string tipoDoc);


    }
}
