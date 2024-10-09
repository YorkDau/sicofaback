using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaMedidaProteccionViolencia
    {
        public long IdMedidaViolencia { get; set; }
        public long? IdMedidaProtecion { get; set; }
        public int? IdTipoViolencia { get; set; }

        public virtual SicofaSolicitudServicioMedidaProtecion? IdMedidaProtecionNavigation { get; set; }
    }
}
