using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Solicitudes
{
    public class RequestDatosInvolucrado1
    {
        public Int64 id { get; set; }

        public string primer_nombre { get; set; } = string.Empty;

        public string segundo_nombre { get; set; } = string.Empty;

        public string primer_apellido { get; set; } = string.Empty;

        public string segundo_apellido { get; set; } = string.Empty;

        //TODO: Eliminar nombres y apellidos ciudadano
        public string nombre_ciudadano { get; set; } = string.Empty;

        public string apellido_ciudadano { get; set; } = string.Empty;

        public int id_tipo_documento { get; set; }

        public string numero_documento { get; set; } = string.Empty;

        public DateTime? fecha_nacimiento { get; set; }

        public string genero { get; set; } = string.Empty;
        public int edad { get; set; }

        public string telefono { get; set; } = string.Empty;

        public string correo_electronico { get; set; } = string.Empty;

        public string localidad { get; set; } = string.Empty;

        public string barrio { get; set; } = string.Empty;

        public string direccion { get; set; } = string.Empty;

        public bool tipoInvolucrado { get; set; }

        public bool principal { get; set; }
        
        public bool tieneSalud { get; set; }
        
        public bool tieneEducacion { get; set; }
        
        public string lugarEstudio { get; set; } = string.Empty;
        
        public bool vacunacionComplete { get; set; }
    }

    public class RequestDatosInvolucradoPrincipal1 : RequestDatosInvolucrado1
    {
        public int? id_tipo_discapacidad { get; set; }

        public string estado_embarazo { get; set; } = string.Empty;

        public string afiliado_seguridad_social { get; set; } = string.Empty;

        public string eps { get; set; } = string.Empty;

        public string ips { get; set; } = string.Empty;


    }
}

