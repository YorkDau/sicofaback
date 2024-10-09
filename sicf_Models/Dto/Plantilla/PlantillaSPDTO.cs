using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Plantilla
{
    public class PlantillaSPDTO
    {
        public string? nombrePlantilla { get; set; }
        public long? idSolPlantilla { get; set; }
        public int? tieneApelacion { get; set; }
        public bool? apelacion { get; set; }
        public bool? aprobado { get; set; }
        public long? idAnexo { get; set; }
        public string? observacion { get; set; }
        public long? idSolicitudServicio { get; set; }
        public long? idSolPSeccion { get; set; }
        public long? idSeccionPlantilla { get; set; }
        public long? idSeccionPadre { get; set; }
        public string? nombreSeccion { get; set; }
        public string? textoSeccion { get; set; }
        public bool? hayInvolucrado { get; set; }
        public string? textoInvolucrado { get; set; }
        public int? orden { get; set; }
        public bool? estadoSeccion { get; set; }
        public bool? aplicaRevision { get; set; }
    }
}
