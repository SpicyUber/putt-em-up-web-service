using Domain;
using Infrastructure.Persistence.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories.Implementations
{
    public class MatchPerformanceRepository : GenericRepository<MatchPerformance>, IMatchPerformanceRepository
    {
        public MatchPerformanceRepository(PuttEmUpDbContext context) : base(context)
        {
        }
    }
}
