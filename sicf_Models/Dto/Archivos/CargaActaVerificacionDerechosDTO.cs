using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Archivos
{
    public class CargaActaVerificacionDerechosDTO
    {
        public long idInvolucrado { get; set; }
        public long idAnexoServicio { get; set; }
        public CargaArchivoDTO archivo { get; set; }
    
    }
}
