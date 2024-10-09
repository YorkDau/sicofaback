using Microsoft.EntityFrameworkCore;

namespace sicf_Models.Core
{
    [Keyless]
    public class SicofaCasosSeguimientos
    {
        public long idSolicitud { get; set; }
        public long idTarea { get; set; }
        public string? codsolicitud { get; set; }
        public string? nombresApellidos { get; set; }
        public string? tipoProceso { get; set; }
        public string? numeroDocumento { get; set; }
        public string? fechaSeguimiento { get; set; }
        public string? codigo { get; set; }
        public string? path { get; set; }
        public string? actividad { get; set; }
        public string? tipoSolicitud { get; set; }
        public string? tipoDocumento { get; set; }
        public string? pathRetorno { get; set; }
    }
}
