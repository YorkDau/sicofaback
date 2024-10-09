using Microsoft.Extensions.Primitives;
using sicf_Models.Dto.Incumplimiento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_DataBase.Repositories.Incumplimiento
{
    public class ReporteIncumplimientoDTO
    {

        public string ciudadSolicitud { get; set; } = string.Empty;

        public ComisariaDTO comisaria { get; set; }

        public InvolucradoIncumplimiento agresor { get; set; }

        public InvolucradoIncumplimiento victima { get; set; }

        public List<InvolucradoIncumplimiento> victimaSecundario { get; set; }

        public IncumplimientoAdicionalDTO? adicional { get; set; }


    }


    public class ComisariaDTO {

        public string nombre { get; set; } = string.Empty;

        public string direccion { get; set; } = string.Empty;

        public string telefono { get; set; } = string.Empty;


        public string correo { get; set; } = string.Empty;

        public ComisariaDTO(string nombre, string direcccion, string telefono, string correo) {

            this.nombre = nombre;
            this.direccion = direcccion;
            this.telefono = telefono;
            this.correo = correo;
        }
  
    }


    public class InvolucradoIncumplimiento { 
        public string nombreCompleto { get; set; } = string.Empty;
            
        public string tipoDocumento { get; set; } = string.Empty;

        public string numeroDocumento { get; set; } = string.Empty;

        public string direccionResidencia { get; set; } = string.Empty;

        public string correoElectronico { get; set; } = string.Empty;

        public string barrio { get; set; } = string.Empty;

        public int edad { get; set; } = 0;

        public string telefono = string.Empty;

        public string tipoRelacion { get; set; } = string.Empty;

    }

    public class SolicitudServicioIncumplimiento {


        public string descripcionHechos { get; set; } = string.Empty; 
    }


    public class TipoViolenciaMedidas { 
    
            public string nombre { get; set; }

            public bool check { get; set; }
            
    
    }
}
