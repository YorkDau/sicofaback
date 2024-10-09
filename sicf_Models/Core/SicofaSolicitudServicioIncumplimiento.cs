using System;
using System.Collections.Generic;

namespace sicf_Models.Core;

public partial class SicofaSolicitudServicioIncumplimiento
{
    public long IdIncumplimiento { get; set; }
    public long IdSolicitudServicio { get; set; }
    public long IdTarea { get; set; }
    public long IdAnexo { get; set; }

    public virtual SicofaIncumplimientoComplementaria SicofaIncumplimientoComplementaria { get; set; } = null!;
}
