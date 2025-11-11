using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaEvaluacionPsicologica
    {
        public long IdEvaluacion { get; set; }
        public long? IdSolicitudServicio { get; set; }
        public long? IdTarea { get; set; }
        public int? RiegosCalculado { get; set; }
        public string? MotivoDescripcion { get; set; }
        public string? AntecedenteDescripcion { get; set; }
        public string? MetodologiaDescripcion { get; set; }
        public string? RelatoHechosDescripcion { get; set; }
        public string? Recomendaciones { get; set; }
        public string? RedApoyoDescripcion { get; set; }
        public string? TipoRedApoyoDescripcion { get; set; }
        public string? PercepcionMujerDescripcion { get; set; }
        public string? PersistenciaDescripcion { get; set; }
        public DateTime? FechaEntrevista { get; set; }
        public DateTime? FechaElaboracionInforme { get; set; }
        public string? ConclusionesEntrevista { get; set; }
        public string? RecomendacionesEntrevista { get; set; }

        public bool? HayMenores { get; set; } = false;
        public string? ValoracionPsicologica { get; set; }
        
        public string? ValoracionEntorno {get; set;}
    }
}
