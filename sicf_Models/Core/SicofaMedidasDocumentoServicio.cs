using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaMedidasDocumentoServicio
    {
        public long IdDocServ { get; set; }
        public long IdSeccionDocumento { get; set; }
        public string TipoMedida { get; set; } = null!;
        public int IdMedida { get; set; }
        public string NomMedida { get; set; } = null!;
        public string Texto { get; set; } = null!;
        public string Estado { get; set; } = null!;
        public string Check { get; set; } = null!;
    }
}
