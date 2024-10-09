using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaTipoAudiencia
    {
        public int IdTipoAudiencia { get; set; }
        public string Descripcion { get; set; } = null!;
        public string? Etiqueta { get; set; }
    }
}
