using Application.DTOs;
using Application.Match.Queries;
using FluentValidation;

namespace Application.Validators
{
    public class MatchSearchParamsValidator : AbstractValidator<SearchMatchesQuery>
    {

        public MatchSearchParamsValidator() {

            RuleFor(x => x.StartDate).NotEmpty().WithMessage("StartDate must not be empty and must be a valid date.").NotNull().WithMessage("StartDate must not be null and must be a valid date.");
            RuleFor(x => x.Mode).NotEmpty().WithMessage("The search 'Mode' must be specified as BeforeIncludingDate, DuringDate or AfterIncludingDate");
        
        }
    }
}
