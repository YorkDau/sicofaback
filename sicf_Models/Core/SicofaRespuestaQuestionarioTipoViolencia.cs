using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaRespuestaQuestionarioTipoViolencia : BaseEntity
    {
        public long IdRespuesta { get; set; }
        public long IdSolicitudServicio { get; set; }
        public int IdQuestionario { get; set; }
        public bool? Mes { get; set; }
        public int Puntuacion { get; set; }

        public long? IdEvaluacionPsicologica { get; set; }

        public virtual SicofaQuestionarioTipoViolencia IdQuestionarioNavigation { get; set; } = null!;
    }
}
