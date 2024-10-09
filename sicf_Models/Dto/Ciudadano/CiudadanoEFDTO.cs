using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Ciudadano
{
    public class CiudadanoEFDTO
    {

        public long IdCiudadano { get; set; }

        public string NombreCiudadano { get; set; }

        public string PrimerApellido { get; set; }

        public string SegundoApellido { get; set; }

        public string tipoDocumento { get; set; }

        public string celular { get; set; }

        public string telefonoFijo { get; set; }

        public int? edad { get; set; }

        public DateTime? fechaNacimiento { get; set; }

        public string? correoElectronico { get; set; }

        public string? numeroDocumento { get; set; }

        public bool? registroCompleto { get; set; }

    }


    public class InvolucradoDTO
    {
        public long IdInvolucrado { get; set; }
        public string? Localidad { get; set; }
        public string NumeroDocumento { get; set; } = null!;
        public int? TipoDocumento { get; set; }
        public string? Nombres { get; set; } = null!;
        public string? Apellidos { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int? Edad { get; set; }
        public int? idGenero { get; set; }
        public string? Telefono { get; set; }
        public string? CorreoElectronico { get; set; }
        public string? Barrio { get; set; }
        public string? DireccionRecidencia { get; set; }
        public bool EsVictima { get; set; }
        public bool? EsPrincipal { get; set; }
        public int? IdTipoDiscpacidad { get; set; }
        public string? EstadoEmbarazo { get; set; }
        public string? AfiliadoSeguridadSocial { get; set; }
        public string? Eps { get; set; }
        public string? Ips { get; set; }

    }




}
