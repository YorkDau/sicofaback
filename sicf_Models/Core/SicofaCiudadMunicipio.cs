using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaCiudadMunicipio
    {
        public SicofaCiudadMunicipio()
        {
            SicofaCiudadano = new HashSet<SicofaCiudadano>();
            SicofaComisaria = new HashSet<SicofaComisaria>();
            SicofaLocalidad = new HashSet<SicofaLocalidad>();
        }

        public long IdCiudadMunicipio { get; set; }
        public long IdDepartamento { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Codigo { get; set; }
        public bool? LlamadaDeVida { get; set; }
        public string? MensajeLlamadaVida { get; set; }

        public virtual SicofaDepartamento IdDepartamentoNavigation { get; set; } = null!;
        public virtual ICollection<SicofaCiudadano> SicofaCiudadano { get; set; }
        public virtual ICollection<SicofaComisaria> SicofaComisaria { get; set; }
        public virtual ICollection<SicofaLocalidad> SicofaLocalidad { get; set; }
    }
}
