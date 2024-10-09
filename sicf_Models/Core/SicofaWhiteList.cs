using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaWhiteList
    {
        public long IdWhiteList { get; set; }
        public string Jwt { get; set; } = null!;
        public DateTime? Fecha { get; set; }
    }
}
