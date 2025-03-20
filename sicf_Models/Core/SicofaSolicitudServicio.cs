using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaSolicitudServicio
    {
        public SicofaSolicitudServicio()
        {
            SicofaApelacion = new HashSet<SicofaApelacion>();
            SicofaProgramacion = new HashSet<SicofaProgramacion>();
            SicofaRemisionSolicitudServicio = new HashSet<SicofaRemisionSolicitudServicio>();
            SicofaSolicitudEstadoSolicitud = new HashSet<SicofaSolicitudEstadoSolicitud>();
            SicofaSolicitudPrueba = new HashSet<SicofaSolicitudPrueba>();
            SicofaSolicitudServicioAnexo = new HashSet<SicofaSolicitudServicioAnexo>();
            SicofaSolicitudServicioMedidaProtecion = new HashSet<SicofaSolicitudServicioMedidaProtecion>();
            SicofaTarea = new HashSet<SicofaTarea>();
            IdInvolucrado = new HashSet<SicofaInvolucrado>();
        }

        public long IdSolicitudServicio { get; set; }
        public int? IdUsuarioSistema { get; set; }
        public long? IdCiudadano { get; set; }
        public long IdComisaria { get; set; }
        public string CodigoSolicitud { get; set; } = null!;
        public DateTime FechaSolicitud { get; set; }
        public DateTime HoraSolicitud { get; set; }
        public string DescripcionDeHechos { get; set; } = null!;
        public bool EsVictima { get; set; }
        public DateTime? FechaHechoViolento { get; set; }
        public bool? ConviveConAgresor { get; set; }
        public int? IdTipoTramite { get; set; }
        public int? IdContextoFamiliar { get; set; }
        public bool? EsCompetenciaComisaria { get; set; }
        public string? NoCompetenciaDescrip { get; set; }
        public bool? EsNecesarioRemitir { get; set; }
        public int? IdRelacionParentescoAgresor { get; set; }
        public string? JustificacionRemision { get; set; }
        public string? LugarHechoViolento { get; set; }
        public string? TipoSolicitud { get; set; }
        public string? EstadoSolicitud { get; set; }
        public string? SubestadoSolicitud { get; set; }

        public virtual SicofaCiudadano? IdCiudadanoNavigation { get; set; }
        public virtual SicofaComisaria IdComisariaNavigation { get; set; } = null!;
        public virtual SicofaUsuarioSistema? IdUsuarioSistemaNavigation { get; set; }
        public virtual SicofaSolicitudServicioComplementaria SicofaSolicitudServicioComplementaria { get; set; } = null!;
        public virtual ICollection<SicofaApelacion> SicofaApelacion { get; set; }
        public virtual ICollection<SicofaProgramacion> SicofaProgramacion { get; set; }
        public virtual ICollection<SicofaRemisionSolicitudServicio> SicofaRemisionSolicitudServicio { get; set; }
        public virtual ICollection<SicofaSolicitudEstadoSolicitud> SicofaSolicitudEstadoSolicitud { get; set; }
        public virtual ICollection<SicofaSolicitudPrueba> SicofaSolicitudPrueba { get; set; }
        public virtual ICollection<SicofaSolicitudServicioAnexo> SicofaSolicitudServicioAnexo { get; set; }
        public virtual ICollection<SicofaSolicitudServicioMedidaProtecion> SicofaSolicitudServicioMedidaProtecion { get; set; }
        public virtual ICollection<SicofaTarea> SicofaTarea { get; set; }

        public virtual ICollection<SicofaInvolucrado> IdInvolucrado { get; set; }
        public ICollection<SicofaObservacionSolicitudApelacion> Observaciones { get; set; }
    }
}
