using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.EvaluacionPsicologica
{
    public class QuestionarioDTO
    {
        public int IdQuestionario { get; set; }
        public int IdTipoViolencia { get; set; }
        public string? Descripcion { get; set; }
        public bool EsCerrada { get; set; }
        public int? Puntuacion { get; set; }
    }
}
