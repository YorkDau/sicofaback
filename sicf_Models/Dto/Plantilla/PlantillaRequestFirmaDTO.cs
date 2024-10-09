using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Plantilla
{
    public class PlantillaRequestFirmaDTO
    {
        public long idSolPlantilla { get; set; }
        public bool? apelacion { get; set; }
        public long? idAnexo { get; set; }
        public bool cierre { get; set; }
    }
}
