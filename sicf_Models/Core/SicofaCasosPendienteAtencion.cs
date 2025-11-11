using Microsoft.EntityFrameworkCore;

namespace sicf_Models.Core
{
    [Keyless]
    public class SicofaCasosPendienteAtencion
    {
        public long idSolicitud { get; set; }
        public long idTarea { get; set; }
        public string codsolicitud { get; set; } = String.Empty;
        public string nombresApellidos { get; set; } = String.Empty;
        public string tipoProceso { get; set; } = String.Empty;
        public string numeroDocumento { get; set; } = String.Empty;
        public string fechaSolicitud { get; set; } = String.Empty;
        public string estado { get; set; } = String.Empty;
        public string? codigo { get; set; }
        public string path { get; set; } = string.Empty;
        public string actividad { get; set; } = string.Empty;
        public string municipioComisaria { get; set; } = string.Empty;
        public string tipoSolicitud { get; set; } = string.Empty;
        public string? pathRetorno { get; set; }
        public int remision { get; set; }
        public string sexoAfectado { get; set; } = string.Empty;
        public string? tipo_presolicitud { get; set; } = string.Empty;
    }
}
