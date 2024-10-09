using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Apelacion
{
    public class ApelacionMedidasDTO
    {
        public int idMedida { get; set; }
        public int tipoMedida { get; set; }
        public string nombreMedida { get; set; }
        public string estadoMedida { get; set; }
        public string? excluir { get; set; }

        public ApelacionMedidasDTO()
        {
            this.nombreMedida = "";
            this.estadoMedida = "";
        }
    }
}
