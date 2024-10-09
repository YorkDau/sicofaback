using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using sicf_Models.Utility;

namespace sicf_Models.Dto.Programacion
{
    public class ProgramacionGuardarDTO
    {
        public long idProgramacion { get; set; }
        public long idSolicitudServicio { get; set; }
        public long idTarea { get; set; }

        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime fechaHoraInicial { get; set; }

        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime fechaHoraFinal { get; set; }
        public int? idTipoAudiencia { get; set; }
    }
}
