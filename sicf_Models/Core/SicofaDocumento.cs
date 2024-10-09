using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaDocumento
    {

        public SicofaDocumento()
        {
            SicofaSolicitudServicioAnexo = new HashSet<SicofaSolicitudServicioAnexo>();
        }

        public int IdDocumento { get; set; }
        public long? IdComisaria { get; set; }
        public string NombreDocumento { get; set; } = null!;
        public decimal? VersionDocumento { get; set; }
        public string? Estado { get; set; }
        public bool? Multiple { get; set; }
        public bool? EsVictima { get; set; }

        public string? Codigo { get; set; }

        public virtual SicofaComisaria? IdComisariaNavigation { get; set; }
        public virtual ICollection<SicofaSolicitudServicioAnexo> SicofaSolicitudServicioAnexo { get; set; }

    }
}
