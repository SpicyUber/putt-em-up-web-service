using Application.DTOs;
using Domain;
using Infrastructure.Persistence.AvatarProvider;
using Infrastructure.Persistence.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Match.Queries
{
    public class SearchMatchesIncludingCancelledQueryHandler : IRequestHandler<SearchMatchesIncludingCancelledQuery,MatchPreviewPage>
    {
        private readonly IUnitOfWork uow;
        private readonly IAvatarProvider ap;

        public SearchMatchesIncludingCancelledQueryHandler(IUnitOfWork uow, IAvatarProvider ap)
        {
            this.uow= uow;
            this.ap= ap;
        }

        public Task<MatchPreviewPage> Handle(SearchMatchesIncludingCancelledQuery request, CancellationToken cancellationToken)
        {
            List<MatchPreview> matchPreviews = new List<MatchPreview>();
            IQueryable<Domain.Match> validMatches =null;
            switch (request.Mode)
            {

                case SearchMode.BeforeIncludingDate:
                    validMatches = FindMatchesBeforeDate(request.StartDate);
                    break;
                case SearchMode.AfterIncludingDate:
                    validMatches = FindMatchesAfterDate(request.StartDate);
                    break;
                case SearchMode.DuringDate:
                    validMatches = FindMatchesDuringDate(request.StartDate);
                    break;
            }
            
            int totalPages = 0;
             if (validMatches == null || validMatches.Count() == 0) return Task.FromResult<MatchPreviewPage>(new MatchPreviewPage(new MatchPreview[0],totalPages));
             

            matchPreviews = CreateMatchPreviews(validMatches,request.PlayerID);
            if (request.PageNumber != null && request.PageSize != null)
            {
                totalPages = (int)Math.Ceiling(matchPreviews.Count / (float)request.PageSize);
                matchPreviews = matchPreviews.Skip((int)(request.PageNumber) * (int)(request.PageSize) - (int)(request.PageSize)).Take((int)(request.PageSize)).ToList<MatchPreview>();
            }
            return Task.FromResult(new MatchPreviewPage(matchPreviews.ToArray(), totalPages));
        }

        private IQueryable<Domain.Match> FindMatchesAfterDate(DateTime startDate )
        {

            return uow.MatchRepository.Query().Where((Domain.Match m) => m.StartDate.Date >= startDate.Date);
             



        }


        private IQueryable<Domain.Match> FindMatchesDuringDate(DateTime startDate)
        {
            return uow.MatchRepository.Query().Where((Domain.Match m) => m.StartDate.Date.Equals( startDate.Date));


        }

        private IQueryable<Domain.Match> FindMatchesBeforeDate(DateTime startDate )
        {
            return uow.MatchRepository.Query().Where((Domain.Match m) => m.StartDate.Date <= startDate.Date );

        }

        private List<MatchPreview> CreateMatchPreviews(IQueryable<Domain.Match> validMatches,long playerId)
        {

            List<MatchPreview> result = new List<MatchPreview>();


            var matchPreviewQueryResult =
                validMatches.Join(uow.MatchPerformanceRepository.Query(), (Domain.Match m) => m.MatchID, (Domain.MatchPerformance m) => m.MatchID,

                (match, matchPerformance1) => new { match, matchPerformance1 })

                .Where((a) => a.matchPerformance1.PlayerID == playerId)

                .Join(uow.MatchPerformanceRepository.Query(), (a) => a.match.MatchID, (Domain.MatchPerformance m) => m.MatchID,

                (a, matchPerformance2) => new { match = a.match, matchPerformance1 = a.matchPerformance1, matchPerformance2 = matchPerformance2 })

                .Where((a) => a.matchPerformance2.PlayerID != playerId)


                .Join(uow.PlayerRepository.Query(), (a) => a.matchPerformance1.PlayerID, (p1) => p1.Id,

                (a, p1) => new { match = a.match, matchPerformance1 = a.matchPerformance1, matchPerformance2 = a.matchPerformance2, player1 = p1 })

                .Join(uow.PlayerRepository.Query(), (a) => a.matchPerformance2.PlayerID, (p2) => p2.Id,

                (a, p2) => new { match = a.match, matchPerformance1 = a.matchPerformance1, matchPerformance2 = a.matchPerformance2, player1 = a.player1, player2 = p2 }).OrderByDescending((a)=>a.match.StartDate);



            foreach (var element in matchPreviewQueryResult) { 

            result.Add(
                new(element.match, element.player1, element.player2,
                element.matchPerformance1, element.matchPerformance2,
                ap.GetAvatar(element.player1.AvatarFilePath), ap.GetAvatar(element.player2.AvatarFilePath)));
            }

            return result;
        }
    }

}
