using System;
using System.Collections.Generic;

namespace sicf_DataBase.Data
{
    public partial class SicofaSolicitudServicioPlantilla
    {
        public SicofaSolicitudServicioPlantilla()
        {
            SicofaSolicitudServicioPsecciones = new HashSet<SicofaSolicitudServicioPseccione>();
        }

        public long IdSolPlantilla { get; set; }
        public long? IdSolicitudServicio { get; set; }
        public long? IdPlantilla { get; set; }
        public int? TieneApelacion { get; set; }
        public string? EstadoPlantilla { get; set; }
        public string? observacion { get; set; }
        public bool? esNecesarioRemitir { get; set; }
        public bool? aprobado { get; set; }
        public bool? apelacion { get; set; }
        public long? idAnexo { get; set; }
        public string? estadoSolicitud { get; set; }
        public bool afectaMedidas { get; set; }

        public virtual SicofaPlantilla? IdPlantillaNavigation { get; set; }
        public virtual ICollection<SicofaSolicitudServicioPseccione> SicofaSolicitudServicioPsecciones { get; set; }
    }
}
