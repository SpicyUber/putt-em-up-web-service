using Application.DTOs;
using Application.Player.Commands;
using FluentValidation;

namespace Putt_Em_Up_Portal.Validators
{
    public class AccountEditParamsValidator : AbstractValidator<EditAccountCommand>
    {

        public AccountEditParamsValidator()
        {

            RuleFor(x => x.AccountParams.Username).Length(2, 32).WithMessage("Username must be between 2 and 32 characters long");
            RuleFor(x => x.AccountParams.Password).Length(6, 32).WithMessage("Password must be between 6 and 32 characters long");
        }


    }
}
