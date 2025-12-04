using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Plantilla
{
    public class PlantillaGuardarDTO
    {
        public string? observacion { get; set; }
        public bool? aprobado { get; set; }
        public bool? esNecesarioRemitir { get; set; }


        public bool? cierre { get; set; }
        public string? observacionCierre { get; set; }
        public string? adjuntoAutoCierre { get; set; }
        public long? idAdjunto { get; set; }
        
        public List<PlantillaSeccionesDTO> secciones { get; set; }

        public PlantillaGuardarDTO()
        {
            this.observacion = "";
        }
    }
}