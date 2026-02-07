using Infrastructure.Persistence.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MatchPerformance.Queries
{
    public class FindMatchPerformanceQueryHandler : IRequestHandler<FindMatchPerformanceQuery, Domain.MatchPerformance>
    {
        private readonly IUnitOfWork uow;

        public FindMatchPerformanceQueryHandler(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public Task<Domain.MatchPerformance> Handle(FindMatchPerformanceQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(uow.MatchPerformanceRepository.Query().FirstOrDefault(mp => mp.PlayerID == request.PlayerID && mp.MatchID == request.MatchID));
        }
    }
}
