using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaProgramacion
    {
        public SicofaProgramacion()
        {
            SicofaSeguimiento = new HashSet<SicofaSeguimiento>();
        }

        public long IdProgramacion { get; set; }
        public long IdSolicitud { get; set; }
        public long IdTarea { get; set; }
        public long? IdTareaUso { get; set; }
        public string Etiqueta { get; set; } = null!;
        public string Razon { get; set; } = null!;
        public DateTime FechaHoraInicial { get; set; }
        public DateTime FechaHoraFinal { get; set; }
        public string Estado { get; set; } = null!;
        public int? UsuarioModifica { get; set; }
        public DateTime? FechaModifica { get; set; }
        public int? IdTipoAudiencia { get; set; }
        public bool? reprogramada { get; set; }
        public bool? faltantes { get; set; }

        public virtual SicofaSolicitudServicio IdSolicitudNavigation { get; set; } = null!;
        public virtual SicofaTarea IdTareaNavigation { get; set; } = null!;
        public virtual SicofaTarea? IdTareaUsoNavigation { get; set; }
        public virtual ICollection<SicofaSeguimiento> SicofaSeguimiento { get; set; }
    }
}
