using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaHijoinvolucrado
    {
        public string? Nombres { get; set; } = "Sin ingresar";
        public long IdHijo { get; set; }
        public long IdInvolucrado { get; set; }
        public int Edad { get; set; }
        public int? EdadEn { get; set; } = 0;
        public int? Custodia { get; set; }
        public int? IdSexo { get; set; }

        public long? IdSolicitudServicio { get; set; }
        public int IdRelacionParental{ get; set; }
    }
}
