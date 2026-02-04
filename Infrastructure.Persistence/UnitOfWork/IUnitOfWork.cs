using Infrastructure.Persistence.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IPlayerRepository  PlayerRepository { get; set; }
        public IMatchRepository MatchRepository { get; set; }

        public IMessageRepository MessageRepository { get; set; }

        public IMatchPerformanceRepository MatchPerformanceRepository { get; set; }

        void SaveChanges();


    }
}
