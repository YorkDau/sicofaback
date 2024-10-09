using Newtonsoft.Json;
using sicf_Models.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Programacion
{
    public class ProgramacionDTO
    {
        public long idProgramacion { get; set; }
        public long idSolicitudServicio { get; set; }
        public long idTarea { get; set; }
        public string etiqueta { get; set; }
        public string razon { get; set; }
        public DateTime fechaHoraInicial { get; set; }
        public DateTime fechaHoraFinal { get; set; }
        public int? idTipoAudiencia { get; set; }
        public List<ProgramacionTipoAudienciaDTO>? listTiposAudiencia { get; set; }
        public List<ProgramacionAgendaDTO>? listaProgramaciones { get; set; }

        public ProgramacionDTO()
        { 
            this.etiqueta = string.Empty;
            this.razon = string.Empty;
        }
    }
}
