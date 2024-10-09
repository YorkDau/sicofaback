using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Solicitudes
{
    public class RequestDatosInvolucrado
    {

        public string? nombre_ciudadano { get; set; }

        public string? apellido_ciudadano { get; set; }

        public string? primer_nombre { get; set; }

        public string? segundo_nombre { get; set; }

        public string? primer_apellido { get; set; }

        public string? segundo_apellido { get; set; }

        public int? id_tipo_documento { get; set; }

        public string numero_documento { get; set; }

        public DateTime? fecha_nacimiento { get; set; }

        public int? genero { get; set; }

        public int? edad { get; set; }

        public string telefono { get; set; }

        public string correo_electronico { get; set; }

        public string localidad { get; set; }

        public string barrio { get; set; }

        public string direccion { get; set; }

        public bool? tipoInvolucrado { get; set; }

        public bool principal { get; set; }
    }

    public class RequestDatosInvolucradoPrincipal : RequestDatosInvolucrado
    {
        public int id_tipo_discapacidad { get; set; }

        public string estado_embarazo { get; set; }

        public string afiliado_seguridad_social { get; set; }

        public string eps { get; set; }

        public string ips { get; set; }


    }
}