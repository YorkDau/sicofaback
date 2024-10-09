using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Ciudadano
{
    public class SolicitudServicioDatosDTO
    {
        public string codigo_solicitud { get; set; } = string.Empty;

        public string id_ciudadano { get; set; } = string.Empty;

        public string nombre_ciudaddano { get; set; } = string.Empty;

        public string fecha_solicitud { get; set; } = string.Empty;

        public string hora_solicitud { get; set; } = string.Empty;

        public string fecha_hecho_violento { get; set; } = string.Empty;

        public string descripcion_de_hechos { get; set; } = string.Empty;

        public bool es_victima { get; set; }

        public string relacionParentescoAgresor { get; set; } = string.Empty; //id dominio

        public bool? conviveConAgresor { get; set; }

        public bool? esCompetenciaComisaria { get; set; }

        public bool? esNecesarioRemitir { get; set; }

        public string idtipoTramite { get; set; } = string.Empty;//id dominio

        public string idContextofamiliar { get; set; } = string.Empty;

        public string noCompetenciaDescripcion { get; set; } = string.Empty;    

    }


}

