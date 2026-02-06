using Application.DTOs;
using Infrastructure.Persistence.AvatarProvider;
using Infrastructure.Persistence.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Player.Queries
{
    public class SearchPlayersQueryHandler : IRequestHandler<SearchPlayersQuery, LeaderboardPage>
    {
        private readonly IUnitOfWork uow;

        private readonly IAvatarProvider ap;
        public SearchPlayersQueryHandler(IUnitOfWork uow,IAvatarProvider ap)
        {
            this.uow = uow; this.ap = ap;
        }
        public Task<LeaderboardPage> Handle(SearchPlayersQuery request, CancellationToken cancellationToken)
        {
            List<Profile> list = new();
            int totalPages = 0;
            IQueryable<Domain.Player> players = uow.PlayerRepository.Query().Where<Domain.Player>((Domain.Player p) => p.AccountDeleted==false && (request.UsernameStartsWith == null || p.DisplayName.ToLower().StartsWith(request.UsernameStartsWith.ToLower())));

          var results =  
                players.Join(uow.MatchPerformanceRepository.Query(), player => player.PlayerID, mp => mp.PlayerID, (player, mp) => new { p =player, mmr =mp.MMRDelta })
                .GroupBy(a => a.p)
                .Select((g) => new {p=g.Key , mmr =g.Sum(a=>a.mmr) });

            if (request.DescendingRanking) results = results.OrderByDescending((r) => r.mmr);
             else results = results.OrderBy((r) => r.mmr);

            if (request.PageSize == null || request.PageNumber == null)
            {

                foreach (var result in results)
                    list.Add(new Profile(result.p,ap.GetAvatar(result.p.AvatarFilePath)));

                return Task.FromResult(new LeaderboardPage(list.ToArray(), totalPages));
            }

            for (int i = 1; i < (int)request.PageNumber; i++)
                results = results.Skip((int)request.PageSize);

            results = results.Take((int)request.PageSize);

            foreach (var result in results)
                list.Add(new Profile(result.p, ap.GetAvatar(result.p.AvatarFilePath)));

            return Task.FromResult(new LeaderboardPage(list.ToArray(), (int)Math.Ceiling(list.Count / (float)request.PageSize)));
        }
    }
}
