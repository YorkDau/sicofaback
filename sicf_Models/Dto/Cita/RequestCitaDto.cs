using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Cita
{
    public class RequestCitaDto
    {
        public long idCita { get; set; }
        public int idComisaria { set; get; }
        public string? nombCiudadano { set; get; }
        public string? primerApellido { set; get; }
        public string? segundoApellido { set; get; }
        public int tipoDocumento { set; get; }
        public string? numeroDocumento { set; get; }
        public string direccResidencia { get; set; }
        public string? telf { set; get; }
        public string? celular { set; get; }
        public string? correoElectronico { set; get; }
        public List<int>  tipoAtencionList{ set; get; }

        public  RequestCitaDto() {

            tipoAtencionList = new List<int>();
            direccResidencia = string.Empty;
        }

    }
}
