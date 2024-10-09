using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Solicitudes
{
    public class RequestListInvolucradosDTO
    {
        public long id { get; set; }


        public List<RequestDatosInvolucrado> involucrados { get; set; }
    }
}
