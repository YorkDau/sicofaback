using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Incumplimiento
{
    public class IncumplimientosDTO
    {
        public long idIncumplimiento { get; set; }
        public long idAnexo { get; set; }
        public long idSolicitudServicio { get; set; }
        public long idTarea { get; set; }
        public IncumplimientoAdicionalDTO? adicional { get; set; }
    }

    public class IncumplimientoAdicionalDTO
    {
        public long idIncumplimiento { get; set; }
        public string? nombreFuncionario { get; set; }
        public string? cargo { get; set; }
        public string? nombreInstitucion { get; set; }
        public string? direccionInstitucion { get; set; }
        public string? correo { get; set; }
        public string? telefono { get; set; }


    }

    public class IncumplimientoReporteRequest
    {
        public long idSolicitudServicio { get; set; }
        public long idUsuario { get; set; }
        public long idComisaria { get; set; }


    }

}
