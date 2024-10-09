using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Compartido
{
    public class ResponseCasosPendienteAtencion
    {
        public long idSolicitud { get; set; }
        public long idTarea { get; set; }
        public string? codsolicitud { get; set; }
        public string? nombresApellidos{ get; set; }
        public string? tipoProceso { get; set; }
        public string? numeroDocumento { get; set; }
        public string? fechaSolicitud { get; set; }
        public string? estado { get; set; }
        public string? codigoDominioEstado { get; set; }
        public string? codigo { get; set; }
        public string? path { get; set; }
        public int? riesgo { get; set; }
        public long ciudadanoID { get; set; }
        public string? actividad { get; set; }
        public string? municipioComisaria { get; set; }
        public string? tipoSolicitud { get; set; }
        public string? pathRetorno { get; set; }

        public string? riesgoCalculado { get; set; }
        public int remision { get; set; }
    }
}
