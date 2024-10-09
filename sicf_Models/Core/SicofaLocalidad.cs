using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaLocalidad
    {
        public SicofaLocalidad()
        {
            SicofaCiudadano = new HashSet<SicofaCiudadano>();
        }

        public int IdLocalidad { get; set; }
        public long? IdCiudadMunicipio { get; set; }
        public string NombreLocalidad { get; set; } = null!;

        public virtual SicofaCiudadMunicipio? IdCiudadMunicipioNavigation { get; set; }
        public virtual ICollection<SicofaCiudadano> SicofaCiudadano { get; set; }
    }
}
