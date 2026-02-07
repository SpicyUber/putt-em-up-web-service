using Application.DTOs;
using Application.Message.Commands;
using FluentValidation;

namespace Application.Validators
{
    public class MessagePostParamsValidator : AbstractValidator<CreateEmptyMessageCommand>
    {
        public MessagePostParamsValidator() {

            RuleFor(m => m).Must((par) =>{ return par.FromPlayerID != par.ToPlayerID; }).WithMessage("Player cannot send message to themselves.");
        
        }
    }
}
