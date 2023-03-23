using ESB.Application.DTOs;
using FluentValidation;

namespace SEGM.Application.Validations
{
    public class RegistroIncendiosDtoValidation : AbstractValidator<RegistroIncendiosDto>
    {
        public RegistroIncendiosDtoValidation()
        {
            RuleFor(x => x.Direccion).NotNull().WithMessage("Direccion esta vacio");
            RuleFor(x => x.Direccion).NotEmpty().WithMessage("Direccion es requerido");
            RuleFor(x => x.IdBomberoCargo).NotNull().WithMessage("IdBomberoCargo esta vacio");
            RuleFor(x => x.IdBomberoCargo).NotEmpty().WithMessage("IdBomberoCargo es requerido");
        }
    }
}