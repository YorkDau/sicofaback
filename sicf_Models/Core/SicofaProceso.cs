using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaProceso
    {
        public SicofaProceso()
        {
            SicofaFlujoV2 = new HashSet<SicofaFlujoV2>();
        }

        public int IdProceso { get; set; }
        public string? NombreProceso { get; set; }
        public string? Estado { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string? CodigoProceso { get; set; }

        public virtual ICollection<SicofaFlujoV2> SicofaFlujoV2 { get; set; }
    }
}
