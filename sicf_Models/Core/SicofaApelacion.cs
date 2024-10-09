using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaApelacion
    {
        public long IdApelacion { get; set; }
        public long IdSolicitudServicio { get; set; }
        public long IdTarea { get; set; }
        public bool? AceptaRecurso { get; set; }
        public bool? DeclaraNulidad { get; set; }
        public long? IdFlujoRetorno { get; set; }
        public string? EstadoApelacion { get; set; }

        public virtual SicofaSolicitudServicio? IdSolicitudServicioNavigation { get; set; }
        public virtual SicofaTarea? IdTareaNavigation { get; set; }
    }
}
