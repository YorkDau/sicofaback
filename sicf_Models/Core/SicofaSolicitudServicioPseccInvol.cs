using System;
using System.Collections.Generic;

namespace sicf_DataBase.Data
{
    public partial class SicofaSolicitudServicioPseccInvol
    {
        public long IdSeccionInvolucrado { get; set; }
        public long? IdSolPlantillaSeccion { get; set; }
        public long? IdInvolucrado { get; set; }
        public bool? EstadoInvolucrado { get; set; }

        public virtual SicofaSolicitudServicioPseccione? IdSolPlantillaSeccionNavigation { get; set; }
    }
}
