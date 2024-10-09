using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Abogado
{
    public class InvolucradosDTO
    {
        public victimaDTO? victima { get; set; } 

        public victimaDTO? agresor { get; set; }

    }

    public class victimaDTO {

        public string nombres { get; set; }

        public string primerNombre { get; set; }

        public string segundoNombre { get; set; }

        public string primerApellido { get; set; }

        public string segundoApellido { get; set; }

        public string apellidos { get; set; }

        public string? tipoDocumento { get; set; }

        public string numeroDocumento { get; set; }

        public string barrio { get; set; }

        public string telefono { get; set; }

        public string correo { get; set; }

        public string parentesco { get; set; }

        public string nivelEstudio{ get; set; }

        public string estadoCivil { get; set; }

        public string ocupacion { get; set; }

        public string lugarExpedicionAgresor { get; set; }

        public string direccion { get; set; }

        public int? edad { get; set; }

        public victimaDTO(string nombres, string primerNombre, string segundoNombre, string primerApellido, string segundoApellido, string apellidos, string? tipoDocumento, string numeroDocumento, string barrio, string telefono, string correo, string parentesco, string nivelEstudio, string estadoCivil, string ocupacion, string lugarExpedicionAgresor, string direccion, int edad)
        {
            this.nombres = nombres;
            this.primerNombre = primerNombre;
            this.segundoNombre = segundoNombre;
            this.primerApellido = primerApellido;
            this.segundoApellido = segundoApellido; 
            this.apellidos = apellidos;
            this.tipoDocumento = tipoDocumento;
            this.numeroDocumento = numeroDocumento;
            this.barrio = barrio;
            this.telefono = telefono;
            this.correo = correo;
            this.parentesco = parentesco;
            this.nivelEstudio = nivelEstudio;
            this.estadoCivil = estadoCivil;
            this.ocupacion = ocupacion;
            this.lugarExpedicionAgresor = lugarExpedicionAgresor;
            this.direccion = direccion;
            this.edad = edad;
        }
    }

    


   
}
