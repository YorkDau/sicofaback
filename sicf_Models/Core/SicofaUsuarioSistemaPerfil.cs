using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaUsuarioSistemaPerfil
    {
       
        public int IdUsuarioSistema { get; set; }
        public int IdPerfil { get; set; }
        public bool? Estado { get; set; }
        //public long? IdComisaria { get; set; }

        public virtual SicofaPerfil IdPerfilNavigation { get; set; } = null!;
        public virtual SicofaUsuarioSistema IdUsuarioSistemaNavigation { get; set; } = null!;
        //public virtual SicofaComisaria? IdComisariaNavigation { get; set; }
    }
}
