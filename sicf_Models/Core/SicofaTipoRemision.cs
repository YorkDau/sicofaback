using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaTipoRemision
    {
        public SicofaTipoRemision()
        {
            SicofaSolicituServicioRemision = new HashSet<SicofaSolicituServicioRemision>();
        }

        public int IdTipoRemision { get; set; }
        public string? Descripcion { get; set; }

        public virtual ICollection<SicofaSolicituServicioRemision> SicofaSolicituServicioRemision { get; set; }
    }
}
