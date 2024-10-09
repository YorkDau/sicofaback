using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaInvolucradosMedidasDocumentoServicio
    {
        public long IdDocServ { get; set; }
        public long IdSeccionDocumento { get; set; }
        public string TipoMedida { get; set; } = null!;
        public int IdMedida { get; set; }
        public long IdInvolucrado { get; set; }
        public string Check { get; set; } = null!;
    }
}
