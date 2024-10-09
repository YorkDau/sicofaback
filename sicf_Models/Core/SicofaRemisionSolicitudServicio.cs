using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaRemisionSolicitudServicio
    {
        public long IdRemision { get; set; }
        public long IdSolicitudServicio { get; set; }
        public long IdComisariaOrigen { get; set; }
        public bool TipoRemision { get; set; }
        public long? IdComisariaDestino { get; set; }
        public long? IdEntidadExterna { get; set; }
        public string? Justificacion { get; set; }
        public int? IdUsuarioSistema { get; set; }

        public virtual SicofaComisaria? IdComisariaDestinoNavigation { get; set; }
        public virtual SicofaComisaria IdComisariaOrigenNavigation { get; set; } = null!;
        public virtual SicofaEntidadExterna? IdEntidadExternaNavigation { get; set; }
        public virtual SicofaSolicitudServicio IdSolicitudServicioNavigation { get; set; } = null!;
        public virtual SicofaUsuarioSistema? IdUsuarioSistemaNavigation { get; set; }
    }
}
