using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaFormatos
    {
        public int IdFormato { get; set; }
        public string NombreDocumento { get; set; } = null!;
        public string Paht { get; set; } = null!;
        public decimal? VersionDocumento { get; set; }
        public string? Estado { get; set; }
        public string? Codigo { get; set; }
    }
}
