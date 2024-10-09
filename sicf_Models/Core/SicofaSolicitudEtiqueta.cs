using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaSolicitudEtiqueta
    {
        public long IdEtiqueta { get; set; }
        public long? IdSolicitud { get; set; }
        public string? Etiqueta { get; set; }
        public string? Estado { get; set; }
        public string? ValorEtiqueta { get; set; }
        public long? IdTarea { get; set; }
    }
}
