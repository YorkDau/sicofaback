using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Plantilla
{
    public class PlantillaSeccionesDTO
    {
        public long? idSolicitudServicio { get; set; }
        public long? idSolPSeccion { get; set; }
        public long? idSeccionPlantilla { get; set; }
        public string? nombreSeccion { get; set; }
        public string? textoSeccion { get; set; }
        public bool? hayInvolucrado { get; set; }
        public string? textoInvolucrado { get; set; }
        public int? orden { get; set; }
        public bool? estadoSeccion { get; set; }
        public List<PlantillaInvolucradoDTO>? involucrados { get; set; }
        public PlantillaSeccionesDTO()
        {
            this.nombreSeccion = "";
            this.textoSeccion = "";
            this.textoInvolucrado = "";
            this.hayInvolucrado = false;
            this.involucrados = new List<PlantillaInvolucradoDTO>();
        }
    }
}
