using Infrastructure.Persistence.Repositories.Implementations;
using Infrastructure.Persistence.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PuttEmUpDbContext context;
        public UnitOfWork(PuttEmUpDbContext context) { this.context = context;

            PlayerRepository = new PlayerRepository(context);
            MatchPerformanceRepository = new MatchPerformanceRepository(context);
            MatchRepository = new MatchRepository(context);
            MessageRepository = new MessageRepository(context);

        }

        public IPlayerRepository PlayerRepository { get; set; }
        public IMatchRepository MatchRepository { get; set; }
        public IMessageRepository MessageRepository { get; set; }
        public IMatchPerformanceRepository MatchPerformanceRepository { get; set; }
        public void SaveChanges()
        {
           context.SaveChanges();
        }
    }
}
