
using Application.DTOs;
using Domain;
using Infrastructure.Persistence.AvatarProvider;
using Infrastructure.Persistence.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.Match.Queries
{
    public class FindMatchByIdQueryHandler : IRequestHandler<FindMatchByIdQuery, MatchPreview>
    {

        private readonly IUnitOfWork uow;
        private readonly IAvatarProvider ap;


        public FindMatchByIdQueryHandler(IUnitOfWork uow, IAvatarProvider ap) { this.uow = uow; this.ap = ap; }
        public Task<MatchPreview> Handle(FindMatchByIdQuery request, CancellationToken cancellationToken)
        {
            Domain.Match match = uow.MatchRepository.GetById(request.Id);
            if (match == null || match.Cancelled) return Task.FromResult<MatchPreview>(null);
            MatchPreview? matchPreview = CreateMatchPreview(match);
            return Task.FromResult(matchPreview);
        }

        private MatchPreview CreateMatchPreview(Domain.Match match)
        {


            IQueryable<Domain.MatchPerformance> matchPerformances = uow.MatchPerformanceRepository.Query()
               .Where(mp =>

                 (match.MatchID == mp.MatchID));

          var listPerformancesWithPlayer = matchPerformances.Join(uow.PlayerRepository.Query(), (Domain.MatchPerformance mp) => mp.PlayerID, (Domain.Player p) => p.Id,
              (Domain.MatchPerformance mp,Domain.Player p) => new { matchPerformance = mp, player = p }).ToList();


            if (listPerformancesWithPlayer.Count < 2) return null;
      

              return new(match, listPerformancesWithPlayer[0].player, listPerformancesWithPlayer[1].player, listPerformancesWithPlayer[0].matchPerformance, listPerformancesWithPlayer[1].matchPerformance,ap.GetAvatar(listPerformancesWithPlayer[0].player.AvatarFilePath), ap.GetAvatar(listPerformancesWithPlayer[1].player.AvatarFilePath));


        }
    }
}
