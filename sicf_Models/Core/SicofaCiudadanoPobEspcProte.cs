using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaCiudadanoPobEspcProte
    {
        public int IdPobEsp { get; set; }
        public long IdCiudadano { get; set; }
        public string? PobIndigena { get; set; }

        public virtual SicofaCiudadano IdCiudadanoNavigation { get; set; } = null!;
        public virtual SicofaPoblacionEspecialProteccion IdPobEspNavigation { get; set; } = null!;
    }
}
