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
        public List<PlantillaSeccionesDTO> secciones { get; set; }

        public PlantillaGuardarDTO()
        {
            this.observacion = "";
        }
    }
}
