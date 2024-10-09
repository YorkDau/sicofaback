using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaSolicitudServicioPrDecreto
    {
        public long IdSolicitudServicioPruebasDecreto { get; set; }
        public long IdSolicitudServicio { get; set; }
        public long? IdMedida { get; set; }
        public long? IdSolicitudServicioAnexo { get; set; }
    }
}
