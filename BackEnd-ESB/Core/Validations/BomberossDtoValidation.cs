using ESB.Application.DTOs;
using FluentValidation;

namespace SEGM.Application.Validations
{
    public class BomberossDtoValidation : AbstractValidator<BomberosDto>
    {
        public BomberossDtoValidation()
        {
            RuleFor(x => x.Nombre).NotNull().WithMessage("Role esta vacio");
            RuleFor(x => x.Nombre).NotEmpty().WithMessage("Role es requerido");
            RuleFor(x => x.Apellido).NotNull().WithMessage("Role esta vacio");
            RuleFor(x => x.Apellido).NotEmpty().WithMessage("Role es requerido");
        }
    }
}