using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Solicitudes
{
    public class PreguntasTipoViolenciaDTO
    {

        public int id_questionario { get; set; }

        public string descripcion { get; set; }

        public bool es_cerrada {get; set;} 


        public int puntuacion { get; set; }
    }
}
