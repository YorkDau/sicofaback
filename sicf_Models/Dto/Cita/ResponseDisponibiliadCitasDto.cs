using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Cita
{
    public class ResponseDisponibiliadCitasDto
    {
        public int idComisaria { set; get; }
        public FechaHoras fechaHoras { get; set; }

        public ResponseDisponibiliadCitasDto() {

            fechaHoras = new FechaHoras();
        }
    }

    public class FechaHoras {

        public DateTime fecha { set; get; }

        public List<HoraCitaDto>? horaCitaDtolist { get; set; }

        public FechaHoras() {

            horaCitaDtolist = new List<HoraCitaDto>();
            fecha = new DateTime();
        }
    }

}
