using System;
using System.Collections.Generic;

namespace sicf_DataBase.Data
{
    public partial class SicofaPlantilla
    {
        public SicofaPlantilla()
        {
            SicofaPlantillaSeccions = new HashSet<SicofaPlantillaSeccion>();
            SicofaSolicitudServicioPlantillas = new HashSet<SicofaSolicitudServicioPlantilla>();
        }

        public long IdPlantilla { get; set; }
        public string? NombreDocumento { get; set; }
        public int? VersionDocumento { get; set; }
        public string? Etiqueta { get; set; }
        public int? TieneApelacion { get; set; }
        public string? Estado { get; set; }
        public string EstadoSolicitud { get; set; }
        public bool afectaMedidas { get; set; }

        public virtual ICollection<SicofaPlantillaSeccion> SicofaPlantillaSeccions { get; set; }
        public virtual ICollection<SicofaSolicitudServicioPlantilla> SicofaSolicitudServicioPlantillas { get; set; }
    }
}
