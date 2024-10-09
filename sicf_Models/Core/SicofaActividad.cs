using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaActividad
    {
        public SicofaActividad()
        {
            SicofaFlujoV2 = new HashSet<SicofaFlujoV2>();
            SicofaPerfilActividad = new HashSet<SicofaPerfilActividad>();
        }

        public int IdActividad { get; set; }
        public string? Etiqueta { get; set; }
        public string? NombreActividad { get; set; }
        public string? Componente { get; set; }
        public bool? Estado { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string? Documento { get; set; }
        public string? ComponenteRetorno { get; set; }
        public bool? AplicaNulidad { get; set; }

        public virtual ICollection<SicofaFlujoV2> SicofaFlujoV2 { get; set; }
        public virtual ICollection<SicofaPerfilActividad> SicofaPerfilActividad { get; set; }
    }
}
