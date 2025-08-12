using Newtonsoft.Json;
using sicf_Models.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Programacion
{
    public class ProgramacionAgendaDTO
    {
        public long IdProgramacion { get; set; }
        public string codigoSolicitud { get; set; }
        public string Audiencia { get; set; } = string.Empty;

        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime FechaHoraInicial { get; set; }

        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime FechaHoraFinal { get; set; }
        public bool esAgendaTarea { get; set; }
    }
}
