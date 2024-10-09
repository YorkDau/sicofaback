using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaSolicitudServicioMedidas
    {
        public long IdSolicitudServicio { get; set; }
        public int TipoMedida { get; set; }
        public int IdMedida { get; set; }
        public string? Estado { get; set; }
        public string? EstadoTmp { get; set; }
        public string? Observacion { get; set; }
        public long? IdAnexoPard { get; set; }
    }
}
