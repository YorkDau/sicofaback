using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaConsecutivos
    {
        public string Anio { get; set; } = null!;
        public decimal Consecutivo { get; set; }
        public string? TipoConsecutivo { get; set; }
    }
}
