using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.EvaluacionPsicologica
{
    public class InformacionVictimaDTO
    {
        public long id { get; set; }

        public string nombres { get; set; } = string.Empty;

        public string primerNombre { get; set; } =  string.Empty;

        public string segundoNombre { get; set; } =  string.Empty;

        public string primerApellido { get; set; } =  string.Empty;

        public string segundoApellido { get; set; } = string.Empty;

        public string apellidos { get; set; } = string.Empty;

        public DateTime? fechaNacimiento { get; set; }

        public DateTime? fechaExpedicion { get; set; }

        public int? tipoDocumento { get; set; }

        public string numeroDocumento { get; set; } = string.Empty;

        public int? sexo { get; set; }

        public int? identidadGenero { get; set; }

        public int? idGenero { get; set; }

        public string ocupacion { get; set; }


        public int? idEscolaridad { get; set; }

        public int? numeroHijos { get; set; }

        public int? relacionAgresor { get; set; }

        public string descripcionRelacionAgresor { get; set; } = string.Empty;

        public bool? agresorConflicto { get; set; }

        public string agresorconflictoDescripcion { get; set; } = string.Empty;

        public int? idDiscapacidad {get; set;}

        public string descripcionDiscapacidad { get; set; } = string.Empty;

        public string embarazo { get; set; } = string.Empty;

        public int? mesesEmbarazo { get; set; }

        public bool? victimaConflicto { get; set; }

        public string eps { get; set; } = string.Empty;

        public string ips { get; set; } = string.Empty;

        public int? cultura { get; set; }

        public int? relacionPareja { get; set; }

        public int? edadAproximadaAgresor { get; set; }
        public string? DireccionRecidencia { get; set; }
        public string? TelefonoContactoConfianza { get; set; }
        public string? Telefono { get; set; }
        public string? Firma { get; set; }

        public long? lugarExpedicion { get; set; }

        public List<informacionHijo> hijos { get; set; } = new List<informacionHijo>();


    }
}
