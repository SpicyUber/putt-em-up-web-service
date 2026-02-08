using Domain;
using Infrastructure.Persistence.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories.Implementations
{
    public class PlayerRepository : GenericRepository<Player>, IPlayerRepository
    {
        public PlayerRepository(PuttEmUpDbContext context) : base(context)
        {
        }

        public Player GetByUsername(string username)
        {
            return context.Set<Player>().SingleOrDefault(p => p.UserName.ToUpper() == username.ToUpper());
        }

        public int GetTotalMatchmakingRanking(long id)
        {
            int result = 0;

            result =  context.Set<Player>().Where((Player p) => p.Id == id)
                .Join(context.Set<MatchPerformance>(),
                (Player p) => p.Id, (MatchPerformance mp) => mp.PlayerID,
                (Player p, MatchPerformance mp) => new { matchId = mp.MatchID, mmrDelta = mp.MMRDelta })
                .Join(context.Set<Match>(), (a) => a.matchId, (Match m) => m.MatchID, (a, m) => new { mmrDelta = a.mmrDelta, cancelled = m.Cancelled })
                .Where((a) => a.cancelled == false).Sum((a) => a.mmrDelta);

            return result;
        }
    }
}
