using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaComisaria
    {
        public SicofaComisaria()
        {
            SicofaCita = new HashSet<SicofaCita>();
            SicofaRemisionSolicitudServicioIdComisariaDestinoNavigation = new HashSet<SicofaRemisionSolicitudServicio>();
            SicofaRemisionSolicitudServicioIdComisariaOrigenNavigation = new HashSet<SicofaRemisionSolicitudServicio>();
            SicofaSolicitudServicio = new HashSet<SicofaSolicitudServicio>();
            //SicofaUsuarioSistema = new HashSet<SicofaUsuarioSistema>();
        }

        public long IdComisaria { get; set; }
        public long IdCiudadMunicipio { get; set; }
        public string CodigoComisaria { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string CorreoElectronico { get; set; } = null!;
        public string? Modalidad { get; set; }
        public string? Naturaleza { get; set; }
        public bool CitaOnline { get; set; }

        public virtual SicofaCiudadMunicipio IdCiudadMunicipioNavigation { get; set; } = null!;
        public virtual ICollection<SicofaCita> SicofaCita { get; set; }
        public virtual ICollection<SicofaDocumento> SicofaDocumento { get; set; }
        public virtual ICollection<SicofaRemisionSolicitudServicio> SicofaRemisionSolicitudServicioIdComisariaDestinoNavigation { get; set; }
        public virtual ICollection<SicofaRemisionSolicitudServicio> SicofaRemisionSolicitudServicioIdComisariaOrigenNavigation { get; set; }
        public virtual ICollection<SicofaSolicitudServicio> SicofaSolicitudServicio { get; set; }
        //public virtual ICollection<SicofaUsuarioSistemaPerfil> SicofaUsuarioSistemaPerfil { get; set; }
        //public virtual ICollection<SicofaUsuarioSistema> SicofaUsuarioSistema { get; set; }
    }
}
