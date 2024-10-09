using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Programacion
{
    public class QuorumDTO
    {
        public long idQuorum { get; set; }
        public long idProgramacion { get; set; }
        public long idTarea { get; set; }
        public long idInvolucrado { get; set; }
        public string nombreInvolucrado { get; set; }
        public long? idAnexo { get; set; }
        public int estado { get; set; }
        public bool esVictima { get; set; }
        public bool esPrincipal { get; set; }
        
        public QuorumDTO()
        {
            this.nombreInvolucrado = "";
        }
    }
}
