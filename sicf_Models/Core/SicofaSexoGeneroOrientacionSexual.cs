using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaSexoGeneroOrientacionSexual
    {
        public SicofaSexoGeneroOrientacionSexual()
        {
            SicofaCiudadanoSexoGeneroOrientacionSexual = new HashSet<SicofaCiudadanoSexoGeneroOrientacionSexual>();
        }

        public int IdSexGenOrient { get; set; }
        public string Nombre { get; set; } = null!;
        public string Tipo { get; set; } = null!;
        public string? Descripcion { get; set; }

        public virtual ICollection<SicofaCiudadanoSexoGeneroOrientacionSexual> SicofaCiudadanoSexoGeneroOrientacionSexual { get; set; }
    }
}
