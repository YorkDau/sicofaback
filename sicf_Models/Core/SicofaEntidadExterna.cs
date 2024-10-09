using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaEntidadExterna
    {
        public SicofaEntidadExterna()
        {
            SicofaRemisionSolicitudServicio = new HashSet<SicofaRemisionSolicitudServicio>();
        }

        public long IdEntidadExterna { get; set; }
        public string? CodigoEntidadExterna { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }

        public virtual ICollection<SicofaRemisionSolicitudServicio> SicofaRemisionSolicitudServicio { get; set; }
    }
}
