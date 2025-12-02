using sicf_Models.Dto.Incumplimiento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Archivos
{
    public class CargaArchivoDTO
    {
        public string entrada { get; set; }
        public string? Nombrearchivo { get; set; }
        public string tipoDocumento { get; set; }
        public long idSolicitudServicio { get; set; }
        public int idUsuario { get; set; }
        public long idComisaria { get; set; }
        public long? idComisariaTraslado { get; set; }
        public int ? idPruebaPericial { get; set; }

    }
    public class EliminarArchivo
    {
        public string Nombrearchivo { get; set; }
    }

    public class ConsultaArchivo
    {
        public string tipoDocumento { get; set; } = string.Empty;
        public long idSolicitudServicio { get; set; }
    }

    public class CargaArchivosRemisionDTO : CargaArchivoDTO 
    { 
        public long idInvolucrado { get; set; }
        public long idTarea { get; set; }
    }

    public class CargaArchivoIncumplimientoDTO : CargaArchivoDTO
    {
        public long idTarea { get; set; }
    }

    public class CargaNotificacionPARD : CargaArchivoDTO
    { 
        public long idDocumento { get; set; }
        
        public long idInvolucrado { get; set; }

        public long idAnexoNotificacionPard { get; set; }
    
    
    }
}
