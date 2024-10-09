using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaMedidas
    {
        public int IdMedida { get; set; }
        public string NomMedida { get; set; } = null!;
        public string Texto { get; set; } = null!;
        public string Estado { get; set; } = null!;
        public int TipoMedida { get; set; }
    }
}
