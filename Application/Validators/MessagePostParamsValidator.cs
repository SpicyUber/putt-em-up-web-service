using Application.DTOs;
using FluentValidation;

namespace Application.Validators
{
    public class MessagePostParamsValidator : AbstractValidator<MessagePostParams>
    {
        public MessagePostParamsValidator() {

            RuleFor(m => m).Must((par) =>{ return par.FromPlayerID != par.ToPlayerID; }).WithMessage("Player cannot send message to themselves.");
        
        }
    }
}
