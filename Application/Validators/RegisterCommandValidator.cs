using FluentValidation;
using Application.DTOs;
using Application.Player.Commands;

namespace Application.Validators
{
    public class RegisterCommandValidator: AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator() {

            RuleFor(x => x.Username).NotEmpty().WithMessage("Username cannot be empty").Length(2,32).WithMessage("Username must be between 2 and 32 characters long");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password cannot be empty").Length(6, 32).WithMessage("Password must be between 6 and 32 characters long");
        
        }
    }
}
