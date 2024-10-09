using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaSeguimiento
    {
        public long IdSeguimiento { get; set; }
        public long IdSolicitudServicio { get; set; }
        public long? IdProgramacion { get; set; }
        public string? Estado { get; set; }
        public int? UsuarioAprueba { get; set; }
        public string? ComentarioAprobacion { get; set; }
        public DateTime? FechaAccion { get; set; }
        public long? IdTareaInstrumentos { get; set; }

        public virtual SicofaProgramacion? IdProgramacionNavigation { get; set; }
    }
}
