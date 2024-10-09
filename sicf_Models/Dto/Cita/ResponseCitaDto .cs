using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Cita
{
    public class ResponseCitaDto
    {
        public string nombComisaria { set; get; }
        public string fechacita { set; get; }
        public string horacita { set; get; }
      
        public ResponseCitaDto() {
            nombComisaria = "";
            fechacita = "";
            horacita = "";
        }

    }
}
