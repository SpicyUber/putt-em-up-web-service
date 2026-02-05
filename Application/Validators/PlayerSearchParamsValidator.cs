using Application.DTOs;
using FluentValidation;

namespace Application.Validators
{
    public class PlayerSearchParamsValidator : AbstractValidator< PlayerSearchParams>
    {
        public PlayerSearchParamsValidator() {
        
        RuleFor(x=>x.PageSize).GreaterThanOrEqualTo(1);
        RuleFor(x=>x.PageNumber).GreaterThanOrEqualTo(1);
            


        }


    }
}
