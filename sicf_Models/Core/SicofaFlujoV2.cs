using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaFlujoV2
    {
        public int IdFlujo { get; set; }
        public int IdProceso { get; set; }
        public int? IdActividadMain { get; set; }
        public string? IdFlujoAnterior { get; set; }
        public int? IdFlujoRetorno { get; set; }
        public string? Operador { get; set; }
        public string? ValorCondicion { get; set; }
        public string? Evento { get; set; }
        public string? TipoFlujo { get; set; }
        public string? Etiqueta { get; set; }
        public string? AccionEtiqueta { get; set; }
        public string? EtiquetaDocumento { get; set; }

        public virtual SicofaActividad? IdActividadMainNavigation { get; set; }
        public virtual SicofaProceso IdProcesoNavigation { get; set; } = null!;
    }
}
