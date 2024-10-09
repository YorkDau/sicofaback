using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Abogado
{
    public class DocumentoRemisionDTO
    {
        public string? nombreVictima { get; set; } = null;

        public string? tipoDocumentoVictima { get; set; } = null;

        public string? numeroDocumentoVictima { get; set; } = null;


        public string? lugarExpedicionVictima { get; set; } = null;

        public string? direccionResidenciaVictima { get; set; } = null;

        public string? barrioVictima { get; set; } = null;

        public string? telefonoVictima { get; set; } = null;

        public string? correoElectronicoVictima { get; set; } = null;


        public string? localidadVictima { get; set; } = null;
        public int? edadVictima { get; set; }

        public string? generoVictima { get; set; }
        public string? informacionFamiliarVictima { get; set; } = null;

        public string? nombreAgresor { get; set; } = null;

        public string? numeroDocumentoAgresor { get; set; } = null;

        public string? tipoDocumentoAgresor { get; set; } = null;

        public string? generoAgresor { get; set; } = null;

        public int? edadAgresor { get; set; } = null;


        public string? direcionResidenciaAgresor { get; set; } = null;

        public string? barrioAgresor { get; set; } = null;

        public string? telefonoAgresor { get; set; } = null;
        public string? correoElectronicoAgresor { get; set; } = null;

        public string? relatoHechos { get; set; } = null;

        public string? ciudadRemision { get; set; }

        public string? direccionComisaria { get; set; }

        public string? telefonoComisaria { get; set; }

        public string? correoComisaria { get; set; }

        public string? nombreComisaria { get; set; }
    }
}
