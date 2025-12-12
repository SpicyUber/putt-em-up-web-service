using FluentValidation;
using Putt_Em_Up_Portal.DTOs;

namespace Putt_Em_Up_Portal.Validators
{
    public class PlayerSearchParamsValidator : AbstractValidator< PlayerSearchParams>
    {
        public PlayerSearchParamsValidator() {
        
        RuleFor(x=>x.PageSize).GreaterThanOrEqualTo(1);
        RuleFor(x=>x.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(x => x.DescendingRanking).NotEmpty();


        }


    }
}
