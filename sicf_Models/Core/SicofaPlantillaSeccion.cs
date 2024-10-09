using System;
using System.Collections.Generic;

namespace sicf_DataBase.Data
{
    public partial class SicofaPlantillaSeccion
    {
        public SicofaPlantillaSeccion()
        {
            SicofaSolicitudServicioPsecciones = new HashSet<SicofaSolicitudServicioPseccione>();
        }

        public long IdSeccionPlantilla { get; set; }
        public long? IdPlantilla { get; set; }
        public long? IdSeccionPadre { get; set; }
        public string? NombreSeccion { get; set; }
        public string? TextoSeccion { get; set; }
        public int? IdMedida { get; set; }
        public bool? HayInvolucrado { get; set; }
        public string? TextoInvolucrado { get; set; }
        public int? Orden { get; set; }
        public string? Estado { get; set; }
        public bool? AplicaSeguimiento { get; set; }
        public string? EstadoSeguimiento { get; set; }

        public virtual SicofaPlantilla? IdPlantillaNavigation { get; set; }
        public virtual ICollection<SicofaSolicitudServicioPseccione> SicofaSolicitudServicioPsecciones { get; set; }
    }
}
