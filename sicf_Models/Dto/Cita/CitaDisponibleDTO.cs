using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Cita
{
    public class CitaDisponibleDTO
    {
        public long idCita { get; set; }

        public DateTime fechaCita { get; set; }

        public DateTime HoraCita { get; set; }

        public bool? activo { get; set; }
    }
}
