using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Plantilla
{
    public class PlantillaInvolucradoDTO
    {
        public long? idSolPSeccion { get; set; }
        public long? idInvolucrado { get; set; }
        public string? nombresInvolucrado { get; set; }
        public string? relacion { get; set; }
        public bool? estado { get; set; }

        public PlantillaInvolucradoDTO()
        {
            this.nombresInvolucrado = "";
            this.estado = false;
        }
    }
}
