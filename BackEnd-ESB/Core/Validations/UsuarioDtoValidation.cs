using ESB.Application.DTOs;
using FluentValidation;
using SEGM.Application.DTOs;

namespace SEGM.Application.Validations
{
    public class UsuarioDtoValidation : AbstractValidator<UsuariosDto>
    {
        public UsuarioDtoValidation()
        {
            RuleFor(x => x.CorreoElectronico).NotNull().WithMessage("Correo no puede estar vacio");
            RuleFor(x => x.CorreoElectronico).NotEmpty().WithMessage("Correo es requerido");
            RuleFor(x => x.CorreoElectronico).EmailAddress().WithMessage("tiene cumplir con lo parametro de Correo");
            RuleFor(p => p.Contraseña).NotEmpty().WithMessage("Su contraseña no puede estar vacía")
              .MinimumLength(8).WithMessage("La longitud de su contraseña debe ser al menos 8.")
              .MaximumLength(16).WithMessage("La longitud de su contraseña no debe exceder 16.")
              .Matches(@"[A-Z]+").WithMessage("Su contraseña debe contener al menos una letra mayúscula.")
              .Matches(@"[a-z]+").WithMessage("Su contraseña debe contener al menos una letra minúscula.")
              .Matches(@"[0-9]+").WithMessage("Su contraseña debe contener al menos un número.")
              .Matches(@"[\@\!\?\*\.]+").WithMessage("Su contraseña debe contener al menos uno (@ ! ? * .).");


            RuleFor(x => x).Custom((x, context) =>
            {
                if (x.Contraseña != x.ConfirmarContraseña)
                {
                    context.AddFailure(nameof(x.Contraseña), "Las contraseñas deben coincidir");
                }
            });
        }
    }
}