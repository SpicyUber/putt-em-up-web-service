using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories.Interfaces
{
    public interface IPlayerRepository : IRepository<Player>
    {
        Player GetByUsername(string username);

        int GetTotalMatchmakingRanking(long id);
    }
}
