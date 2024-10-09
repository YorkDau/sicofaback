using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaCiudadanoSexoGeneroOrientacionSexual
    {
        public long IdCiudadano { get; set; }
        public int IdSexGenOrient { get; set; }
        public string Tipo { get; set; } = null!;

        public virtual SicofaCiudadano IdCiudadanoNavigation { get; set; } = null!;
        public virtual SicofaSexoGeneroOrientacionSexual IdSexGenOrientNavigation { get; set; } = null!;
    }
}
