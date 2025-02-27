using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.EvaluacionPsicologica
{
    public class ActualizacionInvolucradoDTO
    {
        public long IdInvolucrado { get; set; }
        public string ocupacion { get; set; } // aplica para victima // agresor enviarlo ""

        public int Escolidad { get; set; } // aplica para victima , // tambien para agresor pero se puede desconocer 0

        public int RelacionPareja { get; set; } //  aplica solamente para victima , si es agresor 0

        public int numeroHijos { get; set; }    //aplica para ambos

        public int Cultura { get; set; }  // fijo 94

        public int RelacionAgresor { get; set; }  // aplica para la victima

        public string descripcionRelacionAgresor { get; set; }

        public int TipoDiscapcidad { get; set; }  // aplica para victima , si es agresor dejar en 0
        public string descripcionDiscapacidad { get; set; } // aplica para victima si seleciona tipo discapacidad otra, si es agresor dejar en ""

        public List<informacionHijo> informacionHijos { get; set; } // aplica para ambos


        public string embarazo { get; set; } // aplica cuando es victima  true, si es agresor enviar false

        public int mesesEmbarazo { get; set; } // aplica cuando es vicitma true   , si es agresor enviar 0

        public bool victimaConflicto { get; set; } // aplica cuando es victima  true, si es agresor enviar false

        public string eps { get; set; } // aplica cuando es victima , si es agresor enviar "";

        public string ips { get; set; } // aplica cuando es victima , si es agresor enviar "";

        public bool agresorOrganizacionCriminal { get; set; }  // aplica cuando solo cuando es agresor es true , si es victima mandar fijo false

        public string descripcionOrganizacionCriminal { get; set; } // aplica cuando solo cuando es agresor , si es victima enviar ""

        public int? lugarExpedicion { get; set; }
        public int? idSexo { get; set; }

        public int? idRelacionPareja { get; set; }

        // cambios para actualizacion agresor agresor 

        public string? nombres { get; set; }

        public string primerNombre { get; set; }

        public string? segundoNombre { get; set; }

        public string primerApellido { get; set; }

        public string? segundoApellido { get; set; }

        public string? apellidos { get; set; }

        public int? edad { get; set; }

        public int idtipoDocumento { get; set; }

        public string numeroDocumento { get; set; }

        public int? idIdentidadGenero { get; set; }

        public int? edadAproximadaAgresor { get; set; }

        public DateTime? fechaNacimiento { get; set; }

        public DateTime? fechaExpedicion { get; set; }



    }


    public class informacionHijo
    {
        public int? edad { get; set; }

        public int? sexo { get; set; }

        public int? custodia { get; set; }

        public int relacionParental { get; set; }



    }
}
