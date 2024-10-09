using sicf_Models.Dto.Cita;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Presolicitud
{
    public class PresolicitudVERDE
    {
        public long idSolicitudServicio { get; set; } 
        public bool? denunciaVerificada { get; set; } 
        public string? observacionesVerificacion { get; set; } 
        public bool? continuaDenuncia { get; set; }
        public long? idCita { get; set; }
        public DateTime? fechaCita { get; set; }
        public List<PresolicitudTipoViolencia>? listaTiposViolencia { get; set; }
        public List<CitaDto>? listaCitasDisponibles { get; set; }
          

        public PresolicitudVERDE()
        {
            observacionesVerificacion = string.Empty;            
        }
    }
}
