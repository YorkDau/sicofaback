using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaHistorialContrasena
    {
        public long IdHistorial { get; set; }
        public string EncriptPass { get; set; } = null!;
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int? IdUsuario { get; set; }

        public virtual SicofaUsuarioSistema? IdUsuarioNavigation { get; set; }
    }
}
