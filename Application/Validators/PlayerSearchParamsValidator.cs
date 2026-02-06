using Application.DTOs;
using Application.Player.Queries;
using FluentValidation;

namespace Application.Validators
{
    public class PlayerSearchParamsValidator : AbstractValidator< SearchPlayersQuery>
    {
        public PlayerSearchParamsValidator() {
        
        RuleFor(x=>x.PageSize).GreaterThanOrEqualTo(1);
        RuleFor(x=>x.PageNumber).GreaterThanOrEqualTo(1);
            


        }


    }
}
