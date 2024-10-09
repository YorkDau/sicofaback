using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaTarea
    {
        public SicofaTarea()
        {
            SicofaApelacion = new HashSet<SicofaApelacion>();
            SicofaProgramacionIdTareaNavigation = new HashSet<SicofaProgramacion>();
            SicofaProgramacionIdTareaUsoNavigation = new HashSet<SicofaProgramacion>();
            SicofaQuorum = new HashSet<SicofaQuorum>();
            SicofaSolicitudPrueba = new HashSet<SicofaSolicitudPrueba>();
        }

        public long IdTarea { get; set; }
        public int? IdFlujo { get; set; }
        public long? IdSolicitudServicio { get; set; }
        public long? IdTareaAnt { get; set; }
        public int? IdPerfil { get; set; }
        public int? IdUsuarioSistema { get; set; }
        public string? Observaciones { get; set; }
        public string? Estado { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaTerminacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public DateTime? FechaActivacion { get; set; }

        public virtual SicofaPerfil? IdPerfilNavigation { get; set; }
        public virtual SicofaSolicitudServicio? IdSolicitudServicioNavigation { get; set; }
        public virtual SicofaUsuarioSistema? IdUsuarioSistemaNavigation { get; set; }
        public virtual ICollection<SicofaApelacion> SicofaApelacion { get; set; }
        public virtual ICollection<SicofaProgramacion> SicofaProgramacionIdTareaNavigation { get; set; }
        public virtual ICollection<SicofaProgramacion> SicofaProgramacionIdTareaUsoNavigation { get; set; }
        public virtual ICollection<SicofaQuorum> SicofaQuorum { get; set; }
        public virtual ICollection<SicofaSolicitudPrueba> SicofaSolicitudPrueba { get; set; }
    }
}
