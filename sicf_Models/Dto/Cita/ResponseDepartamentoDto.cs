using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Cita
{
    public class ResponseDepartamentoDto
    {
        public long depID { set; get; }
        public string nombDep { set; get; }
        public ResponseDepartamentoDto() {

            nombDep = "";
        }
    }
}
