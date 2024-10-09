using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaTipoDeViolencia :  BaseEntity
    {
        public int IdTipoViolencia { get; set; }
        public string NombTipoViolencia { get; set; } = null!;
    }
}
