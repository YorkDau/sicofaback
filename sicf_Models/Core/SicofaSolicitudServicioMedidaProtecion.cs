using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaSolicitudServicioMedidaProtecion
    {
        public long IdMedida { get; set; }
        public long? IdSolicitudServicio { get; set; }
        public string? NombreTestigo { get; set; }
        public string? CelularTestigo { get; set; }
        public string? DireccionTestigo { get; set; }
        public string? CorreoElectronicoTestigo { get; set; }
        public string? InformacionTexto { get; set; }
        public string? InformacionObservacion { get; set; }
        public string? Pruebas { get; set; }

        public virtual SicofaSolicitudServicio? IdSolicitudServicioNavigation { get; set; }
        public virtual ICollection<SicofaMedidaProteccionViolencia> SicofaMedidaProteccionViolencia { get; set; }


    }
}
