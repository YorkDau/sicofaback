using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.EvaluacionPsicologica
{
    public class Reporte12DTO
    {
        public DatosInstitucionesDTO? institucional { get; set; }
        public InformacionVictimaDTO? victima { get; set; }
        public InformacionVictimaDTO? agresor { get; set; }
        public DescripcionHechosDTO? descripcionHechos { get; set; }
        public List<QuestionarioRespuestaPreviaCorregidaDTO>? tiposViolencia { get; set; }
        public EvaluacionRiegoDTO? valoracion { get; set; }

        public string? nombre_psicologo { get; set; }
        public string? cargo_psicolo { get; set; }

        public string idEscolaridad { get; set; } = string.Empty;

    }
}
