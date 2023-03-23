using ESB.Application.DTOs;
using FluentValidation;

namespace SEGM.Application.Validations
{
    public class RolesDtoValidation : AbstractValidator<RoleDto>
    {
        public RolesDtoValidation()
        {
            RuleFor(x => x.NombreRoles).NotNull().WithMessage("Role esta vacio");
            RuleFor(x => x.NombreRoles).NotEmpty().WithMessage("Role es requerido");
            //RuleFor(x => x.).Length(0, 150).WithMessage("Prioridad lenght 0 to 150 caracters");
        }
    }
}