using AutoMapper;
using sicf_Models.Core;
using sicf_Models.Dto.Abogado;
using sicf_Models.Dto.EvaluacionPsicologica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_DataBase.Mapping
{
    public class PersonProfile : Profile
    {
        public PersonProfile() 
        {
            CreateMap<SicofaQuestionarioTipoViolencia, QuestionarioDTO>();
            CreateMap<EvaluacionPsicologicaDTO, SicofaEvaluacionPsicologica>();

            CreateMap<SicofaTipoRemision,TipoRemisionDTO>();
        }

    }
}
