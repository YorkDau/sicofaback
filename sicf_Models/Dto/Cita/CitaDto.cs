using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Cita
{
    public class CitaDto
    {
        public long idCita { get; set; }
        public long idComisaria { get; set; }
        public string fechaCita { get; set; }
        public string horaCita { get; set; }
       
        public CitaDto()
        {
            fechaCita = "";
            horaCita = "";
        }
    }
}
