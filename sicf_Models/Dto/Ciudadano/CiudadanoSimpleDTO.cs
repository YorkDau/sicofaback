using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Ciudadano
{
    public class CiudadanoSimpleDTO
    {

        public Int64 id { get; set; }

        public string nombre_ciudadano { get; set; }

        public string primer_apellido { get; set; }

        public string segundo_apellido { get; set; }

        public string tipo_documento { get; set; }

        public string celular { get; set; }

        public string telefono_fijo { get; set; }

        public int? edad { get; set; }

        public DateTime? fecha_nacimiento { get; set; }

        public string correo_electronico { get; set; }

        public string numero_documento { get; set; }

        public bool registro_completo { get; set; }

        public string pueblo_indigena { get; set; }

        // public bool poblacion_especial { get; set; }

        public bool poblacion_lgtbi { get; set; }

        public bool nino_nina_adolecente { get; set; }

        public bool victima_conflicto_armado { get; set; }


        public bool persona_lider_defensor_DH { get; set; }

        public bool persona_habitalidad_calle { get; set; }

        public bool migrante { get; set; }

    }
}
