using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaPresolicitud
    {
        public long IdSolicitudServicio { get; set; }
        public long? IdInvolucrado { get; set; }
        public string? DatosAdicionales { get; set; }
        public bool? EsNna { get; set; }
        public string DescripcionHechos { get; set; } = null!;
        public bool DenuncianteAnonimo { get; set; }

        public virtual SicofaInvolucrado? IdInvolucradoNavigation { get; set; }
        public virtual SicofaSolicitudServicio IdSolicitudServicioNavigation { get; set; } = null!;
    }
}
