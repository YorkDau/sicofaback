using FluentValidation;
using FluentValidation.AspNetCore;
using sicf_Models.Dto.Solicitudes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Validation
{
    public class RequestDatosInvolucradoValidator : AbstractValidator<RequestDatosInvolucrado>
    {
        public RequestDatosInvolucradoValidator() {

            RuleFor(x => x.primer_apellido).NotEmpty().WithMessage("Error primer apellido");
            RuleFor(x => x.primer_nombre).NotEmpty().WithMessage("Error primer nombre");

        }
    }
}
