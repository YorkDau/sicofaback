using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaSolicitudServicioAnexo
    {
        public long IdSolicitudAnexo { get; set; }
        public int IdDocumento { get; set; }
        public long IdSolicitudServicio { get; set; }
        public long? IdTarea { get; set; }  
        public bool? Victima { get; set; }
        public string? NombreDocumento { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public int? IdUsuario { get; set; }
        public DateTime? FechaActualizacion { get; set; }

        public long? idInvolucrado { get; set; }

        public virtual SicofaDocumento IdDocumentoNavigation { get; set; } = null!;
        public virtual SicofaSolicitudServicio IdSolicitudServicioNavigation { get; set; } = null!;
    }
}
