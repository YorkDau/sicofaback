using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Cita
{
    public  class DisponibilidadCitaDto
    {
         public string fechaCita { get; set; }

         public List<CitaHora> citaHorasList { get; set; }

        public DisponibilidadCitaDto() {

            citaHorasList = new List<CitaHora>();

            fechaCita = "";

        }
    }

    public class CitaHora {

        public long idCita { get; set; }
        public string  horaCita { get; set; }

        public CitaHora() {

            horaCita = "";
        }
    }
   
}
