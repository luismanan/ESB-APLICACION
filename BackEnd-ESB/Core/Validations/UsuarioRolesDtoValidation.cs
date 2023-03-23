using ESB.Application.DTOs;
using FluentValidation;

namespace SEGM.Application.Validations
{
    public class UsuarioRolesDtoValidation : AbstractValidator<UsuarioRolesDto>
    {
        public UsuarioRolesDtoValidation()
        {
            RuleFor(x => x.IdUsuario).NotNull().WithMessage("IdUsuario no puede estar vacio");
            RuleFor(x => x.IdUsuario).NotEmpty().WithMessage("IdUsuario es requerido");
            RuleFor(x => x.IdRoles).NotNull().WithMessage("IdRoles no puede estar vacio");
            RuleFor(x => x.IdRoles).NotEmpty().WithMessage("IdRoles es requerido");
        }
    }
}