using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Plantilla
{
    public class PlantillaResponse
    {
        public long? idSolPlantilla { get; set; }
        public string nombrePlantilla { get; set; }
        public string observacion { get; set; }
        public int? tieneApelacion { get; set; }
        public bool? aprobado { get; set; }
        public bool? apelacion { get; set; } 
        public long? idAnexo { get; set; }
        public bool? aplicaRevision { get; set; }
        public List<PlantillaSeccionesDTO> secciones { get; set; }
        public object tree { get; set; }
        public PlantillaResponse()
        {
            this.tree = "";
        }

        public bool aplicaMedidas { get; set; }

        public List<long> medidasValidar { get; set; }
    }
}
