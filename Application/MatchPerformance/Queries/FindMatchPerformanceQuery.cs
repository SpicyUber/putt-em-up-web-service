using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MatchPerformance.Queries
{
    public class FindMatchPerformanceQuery : IRequest<Domain.MatchPerformance>
    {
        public long PlayerID { get; set; }
        public long MatchID { get; set; }
    }
}
