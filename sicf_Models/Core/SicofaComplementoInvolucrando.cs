using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaComplementoInvolucrado
    {
        public long IdComplemento { get; set; }
        public long IdInvolucrado { get; set; }
        public int? IdEscolaridad { get; set; }
        public string? Ocupacion { get; set; }
        public int? RelacionPareja { get; set; }
        public int? NumeroHijos { get; set; }
        public int? RelacionAgresor { get; set; }
        public string? DescripcionRelacionAgresor { get; set; }
        public string? DescripcionDiscapacidad { get; set; }
        public int? MesesEmbarazo { get; set; }
        public int? IdCultura { get; set; }
        public bool? AgresorGrupoArmado { get; set; }
        public string? DescripcionGrupoArmado { get; set; }

        public int? EdadAproximadaAgresor { get; set; }

        public virtual SicofaInvolucrado IdInvolucradoNavigation { get; set; } = null!;
    }
}
