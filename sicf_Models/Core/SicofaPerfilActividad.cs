using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaPerfilActividad
    {
        public int IdPerfil { get; set; }
        public int IdActividad { get; set; }
        public string? Estado { get; set; }

        public virtual SicofaActividad IdActividadNavigation { get; set; } = null!;
        public virtual SicofaPerfil IdPerfilNavigation { get; set; } = null!;
    }
}
