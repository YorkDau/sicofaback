using System;
using System.Collections.Generic;

namespace sicf_DataBase.Data
{
    public partial class SicofaSolicitudServicioPseccione
    {
        public SicofaSolicitudServicioPseccione()
        {
            SicofaSolicitudServicioPseccInvols = new HashSet<SicofaSolicitudServicioPseccInvol>();
        }

        public long IdSolPlantillaSeccion { get; set; }
        public long? IdSolicitudServicio { get; set; }
        public long? IdPlantilla { get; set; }
        public long? IdSeccionPlantilla { get; set; }
        public long? IdSeccionPadre { get; set; }
        public string? NombreSeccion { get; set; }
        public string? TextoSeccion { get; set; }
        public int? IdMedida { get; set; }
        public bool? HayInvolucrado { get; set; }
        public string? TextoInvolucrado { get; set; }
        public int? Orden { get; set; }
        public bool? EstadoSeccion { get; set; }
        public bool? AplicaSeguimiento { get; set; }
        public string? EstadoSeguimiento { get; set; }

        public virtual SicofaSolicitudServicioPlantilla? IdPlantillaNavigation { get; set; }
        public virtual SicofaPlantillaSeccion? IdSeccionPlantillaNavigation { get; set; }
        public virtual ICollection<SicofaSolicitudServicioPseccInvol> SicofaSolicitudServicioPseccInvols { get; set; }
    }
}
