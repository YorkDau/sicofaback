using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.EvaluacionPsicologica
{
    public class RegistroRecomendacionDTO
    {

        public long idSolicitudServicio { get; set; } 

        public string decripcion { get; set; } = String.Empty;
    }
}
