using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace sicf_Models.Core
{
    public class SicofaObservacionSolicitudApelacion
    {
        public long Id_observacion_solicitud_apelacion { get; set; }
        public string? Observacion { get; set; } = string.Empty;

        public long Id_solicitud_servicio { get; set; }

        public DateTime Fecha_observacion { get; set; }
        public SicofaSolicitudServicio SicofaSolicitudServicio { get; set; } = null!;
    }
}
