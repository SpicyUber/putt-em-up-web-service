using Application.Player.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class RegisterCommandValidator: AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {

            RuleFor((RegisterCommand x) => x.Password).MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
            RuleFor((RegisterCommand x) => x.Username).MinimumLength(3).WithMessage("Username must be at least 3 characters long.");
        }
    }
}
