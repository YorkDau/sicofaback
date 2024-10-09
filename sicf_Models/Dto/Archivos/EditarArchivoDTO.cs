using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Archivos
{
    public class EditarArchivoDTO
    {

        public string entrada { get; set; }
        public long idSolicitudServicio { get; set; }

        public long idSolicitudServicioAnexo { get; set; }
    }

    public class EliminarArchivoDTO {

        public long idSolicitudServicio { get; set; }

        public long idSolicitudServicioAnexo { get; set; }
    }

    public class EditarPruebaJuez : EditarArchivoDTO
    { 
    
        public long idPrueba { get; set; }
    }
}
