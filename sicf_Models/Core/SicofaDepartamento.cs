using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaDepartamento
    {
        public SicofaDepartamento()
        {
            SicofaCiudadMunicipio = new HashSet<SicofaCiudadMunicipio>();
            SicofaCiudadano = new HashSet<SicofaCiudadano>();
        }

        public long IdDepartamento { get; set; }
        public int? IdPais { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Codigo { get; set; }

        public virtual SicofaPais? IdPaisNavigation { get; set; }
        public virtual ICollection<SicofaCiudadMunicipio> SicofaCiudadMunicipio { get; set; }
        public virtual ICollection<SicofaCiudadano> SicofaCiudadano { get; set; }
    }
}
