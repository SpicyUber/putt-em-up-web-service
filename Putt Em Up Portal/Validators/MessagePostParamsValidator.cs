using FluentValidation;
using Putt_Em_Up_Portal.DTOs;

namespace Putt_Em_Up_Portal.Validators
{
    public class MessagePostParamsValidator : AbstractValidator<MessagePostParams>
    {
        public MessagePostParamsValidator() {

            RuleFor(m => m).Must((MessagePostParams par) =>{ return par.FromPlayerID != par.ToPlayerID; }).WithMessage("Player cannot send message to themselves.");
        
        }
    }
}
