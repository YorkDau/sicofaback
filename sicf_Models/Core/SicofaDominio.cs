using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaDominio
    {

        public int IdDominio { get; set; }
        public string TipoDominio { get; set; } = null!;
        public string? Codigo { get; set; }
        public string? NombreDominio { get; set; }
        public string? TipoLista { get; set; }
        public bool? Activo { get; set; }
        public string? Accion { get; set; }
    }
}
