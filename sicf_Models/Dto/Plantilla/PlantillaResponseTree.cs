using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Plantilla
{
    [Serializable]
    public class PlantillaResponseTree
    {
        public long? idSolPSeccion { get; set; }
        public string? nombreSeccion { get; set; }
        public bool? estado { get; set; }
        public List<PlantillaResponseTree>? leaf { get; set; }
    }
}
