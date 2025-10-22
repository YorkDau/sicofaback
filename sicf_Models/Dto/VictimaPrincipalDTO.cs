using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto
{
    public class VictimaPrincipalDTO
    {
        public long idInvolucrado { get; set; }

        public string nombres { get; set; }

        public string primerNombre { get; set; }

        public string segundoNombre { get; set; }

        public string primerApellido { get; set; }

        public string SegundoApellido { get; set; }

        public string apellidos { get; set; }

        public string tipoDocumento { get; set; }

        public string numeroDocumento { get; set; }

        public DateTime? fechaNacimiento { get; set; }

        public int? edad {get; set;}

        public string eps { get; set; }

        public string escolaridad { get; set; }

        public string telefono { get; set; }
        public string barrio { get; set; }

        public string direccion { get; set; }
        
        public bool? tieneSalud { get; set; } = false;
        public bool? tieneEducacion { get; set; } = false;
        public bool? vacunacionCompleta { get; set; } = false;
        public string? lugarEstudio { get; set; } = string.Empty;
    }
}
