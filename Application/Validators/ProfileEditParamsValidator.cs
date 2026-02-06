using Application.DTOs;
using Application.Player.Commands;
using FluentValidation;

namespace Application.Validators
{
    public class ProfileEditParamsValidator : AbstractValidator<EditProfileCommand>
    {
        public ProfileEditParamsValidator() {
            RuleFor(x => x.Profile).NotNull();
            RuleFor(x => x.Profile.DisplayName).NotEmpty().WithMessage("DisplayName cannot be empty.").Length(2, 32).WithName("DisplayName must be between 2 and 32 characters long.");
            RuleFor(x => x.Profile.Description).MaximumLength(150).WithMessage("Description cannot be longer than 150 characters.");
             
        }
    }
}
