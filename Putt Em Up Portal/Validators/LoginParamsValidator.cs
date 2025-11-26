using FluentValidation;
using Putt_Em_Up_Portal.DTOs;
using Putt_Em_Up_Portal.Models;
using Putt_Em_Up_Portal.Testing;

namespace Putt_Em_Up_Portal.Validators
{
    public class LoginParamsValidator: AbstractValidator<LoginParams>
    {
        public LoginParamsValidator() {

            RuleFor(x => x.Username).NotEmpty().WithMessage("Username cannot be empty").Length(2,32).WithMessage("Username must be between 2 and 32 characters long");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password cannot be empty").Length(6, 32).WithMessage("Password must be between 6 and 32 characters long");
        
        }
    }
}
