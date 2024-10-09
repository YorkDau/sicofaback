using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaSolicituServicioRemision
    {
        public long IdSolicitudRemision { get; set; }
        public int IdTipoRemision { get; set; }
        public bool Personalizada { get; set; }
        public string? Descripcion { get; set; }
        public long? IdInvolucrado { get; set; }

        public virtual SicofaInvolucrado? IdInvolucradoNavigation { get; set; }
        public virtual SicofaTipoRemision IdTipoRemisionNavigation { get; set; } = null!;
    }
}
