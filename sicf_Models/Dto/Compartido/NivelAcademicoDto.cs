using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Compartido
{
    public class NivelAcademicoDto
    {
        public int id { get; set; }
        public string nivelAcademico { get; set; }

        public NivelAcademicoDto() {

            nivelAcademico = string.Empty;
        }

    }
}
