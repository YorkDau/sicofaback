using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Cita
{
    public class HoraCitaDto
    {
        public int idhoraCita { get; set; }
        public string hora_atencion { get; set; }

        public HoraCitaDto() {

            hora_atencion = "";
        }
    }
}
