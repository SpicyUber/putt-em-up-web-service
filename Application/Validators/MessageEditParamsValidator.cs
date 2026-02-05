using Application.DTOs;
using FluentValidation;

namespace Application.Validators
{
    public class MessageEditParamsValidator : AbstractValidator<MessageEditParams>
    {
        public MessageEditParamsValidator() { 
        
        RuleFor(x=>x.Content).MaximumLength(150).WithMessage("Message character limit reached. Please do not go over 150 characters.");
        }
    }
}
