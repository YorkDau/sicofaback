using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaPerfil
    {
        public SicofaPerfil()
        {
            SicofaPerfilActividad = new HashSet<SicofaPerfilActividad>();
            SicofaTarea = new HashSet<SicofaTarea>();
            SicofaUsuarioSistemaPerfil = new HashSet<SicofaUsuarioSistemaPerfil>();
        }

        public int IdPerfil { get; set; }
        public string? NombrePerfil { get; set; }
        public bool? Estado { get; set; }
        public string? Codigo { get; set; }

        public virtual ICollection<SicofaPerfilActividad> SicofaPerfilActividad { get; set; }
        public virtual ICollection<SicofaTarea> SicofaTarea { get; set; }
        public virtual ICollection<SicofaUsuarioSistemaPerfil> SicofaUsuarioSistemaPerfil { get; set; }
    }
}
