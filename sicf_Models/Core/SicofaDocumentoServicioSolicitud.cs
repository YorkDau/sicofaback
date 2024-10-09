using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaDocumentoServicioSolicitud
    {
        public SicofaDocumentoServicioSolicitud()
        {
        }

        public long IdDocServ { get; set; }
        public long IdSolicitudServicio { get; set; }
        public long IdDocumento { get; set; }
        public long? IdComisaria { get; set; }
        public string? Comentarios { get; set; }
        public bool? Personalizada { get; set; }
        public long? IdInvolucrado { get; set; }
        public long? IdTarea { get; set; }
        public string? AprobacionComisario { get; set; }
        public int? IdEstado { get; set; }
        public long? IdAnexo { get; set; }
        public bool? TieneApelacion { get; set; }

    }
}
