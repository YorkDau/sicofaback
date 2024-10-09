using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Cita
{
    public class ResponseComisariaDto
    {
        public string ciudadMunicipio { set; get; }
        public long comisariaID { set; get; }

        public string nombComisaria { set; get; }

        public string direccion { set; get; }

        public string telefono { set; get; }

        public string celular { set; get; }

        public string correo_electronico { set; get; }

        public string horarioSemanal { set; get; }

        public bool dispAgenda { set; get; }

        public bool cita_online { set; get; }

        public List<DisponibilidadCitaDto> disponibilidadCitasList { set; get; }

        public ResponseComisariaDto() {

            disponibilidadCitasList = new List<DisponibilidadCitaDto>();

            ciudadMunicipio = "";

            nombComisaria = "";

            direccion = "";

            telefono = "";

            celular = "";

            correo_electronico = "";

            horarioSemanal = "";

            cita_online = false;
        }
    }
}
