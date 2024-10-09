using Microsoft.EntityFrameworkCore;

namespace sicf_Models.Core
{
    [Keyless]
    public class SicofaCodigoSolicitudSeguimiento
    {
        public long IdSolicitud { get; set; }
        public string CodigoSolicitud { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
    }
}
