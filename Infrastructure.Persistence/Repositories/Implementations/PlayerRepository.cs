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
            return context.Set<Player>().Find(username);
        }
    }
}
