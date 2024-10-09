using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Cita
{
    public class RequestAtenderCitaDto
    {
        public long idCiudadano { get; set; }

       
        public RequestAtenderCitaDto()
        {
            idCiudadano = default;
        }
    }
}
