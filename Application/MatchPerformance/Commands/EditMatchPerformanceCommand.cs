using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MatchPerformance.Commands
{
    public class EditMatchPerformanceCommand() : IRequest<Domain.MatchPerformance>
    {
        public long PlayerID  { get; set; }
        public long MatchID  { get; set; }
        public MatchPerformanceEditParams EditParams {get; set;}
    }
}
