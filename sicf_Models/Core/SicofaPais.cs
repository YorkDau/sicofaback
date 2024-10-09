using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaPais
    {
        public SicofaPais()
        {
            SicofaCiudadano = new HashSet<SicofaCiudadano>();
            SicofaDepartamento = new HashSet<SicofaDepartamento>();
        }

        public int IdPais { get; set; }
        public string NombrePais { get; set; } = null!;
        public string? CodigoPais { get; set; }

        public virtual ICollection<SicofaCiudadano> SicofaCiudadano { get; set; }
        public virtual ICollection<SicofaDepartamento> SicofaDepartamento { get; set; }
    }
}
