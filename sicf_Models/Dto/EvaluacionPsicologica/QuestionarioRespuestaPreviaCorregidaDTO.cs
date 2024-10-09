using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.EvaluacionPsicologica
{
    public class QuestionarioRespuestaPreviaCorregidaDTO
    {
        public int IdQuestionario { get; set; }
        public int IdTipoViolencia { get; set; }
        public string? TipoViolencia { get; set; }
        public string? Descripcion { get; set; }
        public bool EsCerrada { get; set; }
        public int? Puntuacion { get; set; }

        public int? PuntuacionPrevio { get; set; }

        public bool? mesPrevio { get; set; }
    }
}