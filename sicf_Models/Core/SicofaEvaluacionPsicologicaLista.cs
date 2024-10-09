using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaEvaluacionPsicologicaLista
    {
        public long IdLista { get; set; }
        public long? IdEvaluacion { get; set; }
        public int? NombreDominio { get; set; }
        public int? IdDominio { get; set; }
        public bool? Respuesta { get; set; }
    }
}
