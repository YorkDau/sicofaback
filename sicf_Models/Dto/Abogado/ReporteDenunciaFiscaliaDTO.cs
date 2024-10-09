using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Abogado
{
    public class ReporteDenunciaFiscaliaDTO
    {
      
        public long idInvolucrado { get; set; }
        public string? nombres { get; set; }

        public string? tipoDocumento { get; set; }
        public string? numeroDocumento { get; set; }

        public int? edad { get; set; }

        public string? genero { get; set; }

        public string? direccionResidencia { get; set; }

        public string? barrio { get; set; }

        public string? telefono { get; set; }

        public string correoCorreoElectronico { get; set; }
        public bool? esVictima { get; set; }

        public bool? esPrincipal { get; set; }

        public string? lugarExpedicion {get; set;}

        public string? informacionFamiliar { get; set; } = null;

        public DateTime? fechaNacimiento { get; set; }


    }
}
