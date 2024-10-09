using System;
using System.Collections.Generic;

namespace sicfServicesApi.Data
{
    public partial class SicofaUsuarioSistema
    {
        public int IdUsuarioSistema { get; set; }
        public string NumeroDocumento { get; set; } = null!;
        public int IdTipoDocumento { get; set; }
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
    }
}
