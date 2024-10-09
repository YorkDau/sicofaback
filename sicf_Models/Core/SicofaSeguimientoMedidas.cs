using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaSeguimientoMedidas
    {
        public long IdSeguimientoMedidas { get; set; }
        public long IdSolicitudServicio { get; set; }
        public long? IdProgramacion { get; set; }
        public int IdMedida { get; set; }
        public string? EstadoMedida { get; set; }
        public DateTime? Prorroga { get; set; }
        public string? JustificacionProrroga { get; set; }
        public int? UsuarioAprueba { get; set; }
        public int? UsuarioModifica { get; set; }
        public DateTime? FechaModifica { get; set; }
        public long? IdSolicitudAnexo { get; set; }
    }
}
