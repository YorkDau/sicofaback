using sicf_Models.Dto.Compartido;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.EvaluacionPsicologica
{
    public class Reporte17DTO
    {
        public VictimaPrincipalDTO? DatosIdentificacion1 { get; set; }
        public EvaluacionPsicologicaEntrevistaDTO? DatosIdentificacion2 { get; set; }

        public ObtenerEvaluacionPsicologicaEmocionalDTO? Motivo { get; set; }

        public ObtenerEvaluacionPsicologicaEmocionalDTO? AntecendentesYSituacionActual { get; set; }

        public ObtenerEvaluacionPsicologicaEmocionalDTO? ProcedimientoMetodologia { get; set; }

        public ObtenerEvaluacionPsicologicaEmocionalDTO? RelatoDeLosHechos { get; set; }

        public ObtenerEvaluacionPsicologicaEmocionalDTO? RedesApoyo1 { get; set; }
        public List<EvaluacionOrientacionRespuesta>? RedesApoyo2 { get; set; }
        public List<EvaluacionOrientacionRespuesta>? RedesApoyo3 { get; set; }

        public ObtenerEvaluacionPsicologicaEmocionalDTO? PercepcionDeLaVíctima1 { get; set; }
        public List<EvaluacionOrientacionRespuesta>? PercepcionDeLaVíctima2 { get; set; }

        public FuncionarioDTO? funcionario { get; set; } 
        public ObtenerEvaluacionPsicologicaEmocionalDTO? ConclusionYRecomendacion { get; set; }

    }
}
