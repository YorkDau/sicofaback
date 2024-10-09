using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaIncumplimientoComplementaria
    {
        public long IdIncumplimiento { get; set; }
        public string? NombreFuncionario { get; set; }
        public string? Cargo { get; set; }
        public string? NombreInstitucion { get; set; }
        public string? DireccionInstitucion { get; set; }
        public string? Email { get; set; }
        public string? Telefono { get; set; }

        public virtual SicofaSolicitudServicioIncumplimiento IdIncumplimientoNavigation { get; set; } = null!;
    }
}
