using System;
using System.Collections.Generic;

namespace sicf_Models.Core;

public partial class SicofaQuorum
{
    public long IdQuorum { get; set; }
    public long IdProgramacion { get; set; }
    public long IdSolicitudServicio { get; set; }
    public long? IdInvolucrado { get; set; }
    public long? IdAnexo { get; set; }
    public int? IdEstado { get; set; }
    public long IdTarea { get; set; }
    public bool? EsVictima { get; set; }
    public bool? EsPricipal { get; set; }

    public virtual SicofaInvolucrado? IdInvolucradoNavigation { get; set; }
    public virtual SicofaTarea IdTareaNavigation { get; set; } = null!;
}
