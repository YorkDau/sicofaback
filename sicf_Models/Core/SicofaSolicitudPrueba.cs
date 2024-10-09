using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaSolicitudPrueba
    {
        public long IdSolicitudPrueba { get; set; }
        public long IdSolicitudServicio { get; set; }
        public long IdTarea { get; set; }
        public string? TipoPrueba { get; set; }
        public long? IdInvolucrado { get; set; }
        public long? IdAnexo { get; set; }
        public string? NombreArchivo { get; set; }
        public virtual SicofaInvolucrado? IdInvolucradoNavigation { get; set; }
        public virtual SicofaSolicitudServicio IdSolicitudServicioNavigation { get; set; } = null!;
        public virtual SicofaTarea IdTareaNavigation { get; set; } = null!;
    }
}
