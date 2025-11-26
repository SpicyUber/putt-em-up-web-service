using FluentValidation;
using Putt_Em_Up_Portal.DTOs;

namespace Putt_Em_Up_Portal.Validators
{
    public class ProfileEditParamsValidator : AbstractValidator<ProfileEditParams>
    {
        public ProfileEditParamsValidator() {

            RuleFor(x => x.DisplayName).NotEmpty().WithMessage("DisplayName cannot be empty.").Length(2, 32).WithName("DisplayName must be between 2 and 32 characters long.");
            RuleFor(x => x.Description).MaximumLength(150).WithMessage("Description cannot be longer than 150 characters.");
             
        }
    }
}
