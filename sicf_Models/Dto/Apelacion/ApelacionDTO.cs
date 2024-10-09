using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Apelacion
{
    public class ApelacionDTO
    {
        public long idSolicitudServicio { get; set; }
        public long idTarea { get; set; }
        public long? idApelacion { get; set; }
        public bool aceptaRecurso { get; set; }
        public bool declaraNulidad { get; set; }
        public int? idFlujoRetorno { get; set; }
        public List<ApelacionMedidasDTO>? lstMedidas { get; set; }

        public ApelacionDTO()
        {
            this.aceptaRecurso = false;
            this.declaraNulidad = false;
        }
    }
}
