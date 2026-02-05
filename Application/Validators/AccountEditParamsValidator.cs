using Application.DTOs;
using FluentValidation;

namespace Putt_Em_Up_Portal.Validators
{
    public class AccountEditParamsValidator : AbstractValidator<AccountEditParams>
    {

        public AccountEditParamsValidator()
        {

            RuleFor(x => x.Username).Length(2, 32).WithMessage("Username must be between 2 and 32 characters long");
            RuleFor(x => x.Password).Length(6, 32).WithMessage("Password must be between 6 and 32 characters long");
        }


    }
}
