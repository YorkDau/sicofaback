using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaQuestionarioTipoViolencia : BaseEntity
    {
        public SicofaQuestionarioTipoViolencia()
        {
            SicofaRespuestaQuestionarioTipoViolencia = new HashSet<SicofaRespuestaQuestionarioTipoViolencia>();
        }

        public int IdQuestionario { get; set; }
        public int IdTipoViolencia { get; set; }
        public string? Descripcion { get; set; }
        public bool EsCerrada { get; set; }
        public int? Puntuacion { get; set; }

        public virtual ICollection<SicofaRespuestaQuestionarioTipoViolencia> SicofaRespuestaQuestionarioTipoViolencia { get; set; }
    }
}
