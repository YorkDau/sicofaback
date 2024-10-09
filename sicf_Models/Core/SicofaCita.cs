using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaCita
    {
        public long IdCita { get; set; }
        public long IdComisaria { get; set; }
        public long? IdCiudadano { get; set; }
        public DateTime FechaCita { get; set; }
        public DateTime HoraCita { get; set; }
        public int Estado { get; set; }
        public bool? Activo { get; set; }
        public string? OrigenCita { get; set; }

        public virtual SicofaCiudadano? IdCiudadanoNavigation { get; set; }
        public virtual SicofaComisaria IdComisariaNavigation { get; set; } = null!;
    }
}
