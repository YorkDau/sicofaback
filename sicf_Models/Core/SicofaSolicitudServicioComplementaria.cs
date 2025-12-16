using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaSolicitudServicioComplementaria
    {
        public long IdSolicitudServicio { get; set; }
        public int? IdTipoEntidad { get; set; }
        public int? IdTipoDocumentoDenunciante { get; set; }
        public string? NumeroDocumentoDenunciante { get; set; }
        public string? NombresDenunciante { get; set; }
        public string? CorreoDenunciante { get; set; }
        public string? TelefonoDenunciante { get; set; }
        public bool? EsPard { get; set; }
        public bool? TrasladoPard { get; set; }
        public long? IdAnexo { get; set; }
        public string? TipoPresolicitud { get; set; }
        public string? ObservacionLegal { get; set; }
        public long? IdAnexoAutoTramite { get; set; }
        public bool? DenunciaVerificada { get; set; }
        public string? ObservacionVerificacion { get; set; }
        public bool? ContinuaDenuncia { get; set; }
        public long? IdCita { get; set; }
        public long? IdSolicitudRelacionado { get; set; }

        public bool? CompetenciaIcbf { get; set; }

        public string? ObservacionesCompetenciaIcbf { get; set; }
        public string? ObservacionesComisario { get; set; }

        public virtual SicofaSolicitudServicio IdSolicitudServicioNavigation { get; set; } = null!;


        public long? IdEntidadTraslado { get; set; }
        public long? IdAdjuntoConstanciaTraslado { get; set; }
        public string? HechosExistentes { get; set; }
        public string? JustificacionTraslado { get; set; }
        public bool? SeguirTramitePrevencion { get; set; }

        public long? IdAdjuntoInstrumento { get; set; }
        
    }
}
