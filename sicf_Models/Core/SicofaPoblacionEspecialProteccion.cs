using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaPoblacionEspecialProteccion
    {
        public SicofaPoblacionEspecialProteccion()
        {
            SicofaCiudadanoPobEspcProte = new HashSet<SicofaCiudadanoPobEspcProte>();
        }

        public int IdPobEsp { get; set; }
        public string NombPobEsp { get; set; } = null!;

        public virtual ICollection<SicofaCiudadanoPobEspcProte> SicofaCiudadanoPobEspcProte { get; set; }
    }
}
