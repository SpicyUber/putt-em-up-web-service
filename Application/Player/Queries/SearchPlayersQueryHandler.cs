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

            var validPerformances = uow.MatchPerformanceRepository.Query()
            .Join(uow.MatchRepository.Query(),
            mp => mp.MatchID,
            m => m.MatchID,
            (mp, m) => new { mp, m })
        .   Where(a => !a.m.Cancelled)
            .Select(a => a.mp);

            var results = players
            .GroupJoin(
            validPerformances,
            player => player.PlayerID,
            mp => mp.PlayerID,
            (player, matchPerformances) => new
            {
            p = player,
            mmr = matchPerformances.Sum(mp => (int?)mp.MMRDelta) ?? 0
            });

            if (request.DescendingRanking) results = results.OrderByDescending((r) => r.mmr);
             else results = results.OrderBy((r) => r.mmr);

            if (request.PageSize == null || request.PageNumber == null)
            {

                foreach (var result in results)
                    list.Add(new Profile(result.p,ap.GetAvatar(result.p.AvatarFilePath)));

                return Task.FromResult(new LeaderboardPage(list.ToArray(), totalPages));
            }
            totalPages = (int)Math.Ceiling(results.Count() / (float)request.PageSize);
            for (int i = 1; i < (int)request.PageNumber; i++)
                results = results.Skip((int)request.PageSize);

            results = results.Take((int)request.PageSize);

            foreach (var result in results)
                list.Add(new Profile(result.p, ap.GetAvatar(result.p.AvatarFilePath)));

            return Task.FromResult(new LeaderboardPage(list.ToArray(), totalPages));
        }
    }
}
