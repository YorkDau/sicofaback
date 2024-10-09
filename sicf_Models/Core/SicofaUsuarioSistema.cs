using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaUsuarioSistema
    {
        public SicofaUsuarioSistema()
        {
            SicofaRemisionSolicitudServicio = new HashSet<SicofaRemisionSolicitudServicio>();
            SicofaSolicitudServicio = new HashSet<SicofaSolicitudServicio>();
            SicofaTarea = new HashSet<SicofaTarea>();
            SicofaUsuarioSistemaPerfil = new HashSet<SicofaUsuarioSistemaPerfil>();
        }

        public int IdUsuarioSistema { get; set; }
        public string NumeroDocumento { get; set; } = null!;
        public int IdTipoDocumento { get; set; }

       // public long IdComisaria { get; set; }
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string CorreoElectronico { get; set; } = null!;
        public string EncriptPassw { get; set; } = null!;
        public string Cargo { get; set; } = null!;
        public long? TelefonoFijo { get; set; }
        public long? Celular { get; set; }
        public string? IdKeycloak { get; set; }
        public bool? Activo { get; set; }
        public DateTime? FechaCreacion { get; set; }

        public bool? cambioPass { get; set; } 

       // public virtual SicofaComisaria IdComisariaNavigation { get; set; } = null!;
        public virtual ICollection<SicofaRemisionSolicitudServicio> SicofaRemisionSolicitudServicio { get; set; }
        public virtual ICollection<SicofaSolicitudServicio> SicofaSolicitudServicio { get; set; }
        public virtual ICollection<SicofaTarea> SicofaTarea { get; set; }
        public virtual ICollection<SicofaUsuarioSistemaPerfil> SicofaUsuarioSistemaPerfil { get; set; }

        public virtual ICollection<SicofaHistorialContrasena> SicofaHistorialContrasena { get; set; }
    }
}
