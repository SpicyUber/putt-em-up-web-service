using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MatchPerformance.Commands
{
    public class CreateEmptyMatchPerformanceCommand : IRequest<Domain.MatchPerformance>
    {
        public long PlayerID { get; set; }
        public long MatchID { get; set; }

    }
}
