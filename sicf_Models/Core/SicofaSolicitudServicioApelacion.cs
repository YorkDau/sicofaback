using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
   
        public partial class SicofaSolicitudServicioApelacion
        {
            public long IdSolicitudApelacion { get; set; }
            public long IdSolicitudServicio { get; set; }
            public long? IdTarea { get; set; }
            public string? Estado { get; set; }
            public bool? AceptaRecurso { get; set; }
            public bool? DeclaraNulidad { get; set; }
            public long? TareaRetomar { get; set; }
    }
    
}
