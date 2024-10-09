using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaSolicitudRemitida : BaseEntity
    {
        public long IdSolicitudRemitida { get; set; }
        public long IdSolicitudServicio { get; set; }
        public long? IdEntidadExterna { get; set; }
        public long? IdComisaria { get; set; }
        public int IdRemitente { get; set; }
        public string JustificacionRemision { get; set; } = null!;
        public DateTime FechaRemision { get; set; }

        public virtual SicofaEntidadExterna? IdEntidadExternaNavigation { get; set; }
        public virtual SicofaUsuarioSistema IdRemitenteNavigation { get; set; } = null!;
        public virtual SicofaSolicitudServicio IdSolicitudServicioNavigation { get; set; } = null!;
    }
}
