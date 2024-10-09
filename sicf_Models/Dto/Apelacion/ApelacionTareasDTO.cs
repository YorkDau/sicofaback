using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Apelacion
{
    public class ApelacionTareasDTO
    {
        public long idFlujo { get; set; }
        public string nombreTarea { get; set; }

        public ApelacionTareasDTO()
        {
            this.nombreTarea = "";
        }
    }
}
