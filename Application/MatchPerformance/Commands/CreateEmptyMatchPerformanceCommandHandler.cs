using Infrastructure.Persistence.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MatchPerformance.Commands
{
    public class CreateEmptyMatchPerformanceCommandHandler : IRequestHandler<CreateEmptyMatchPerformanceCommand, Domain.MatchPerformance>
    {
        private readonly IUnitOfWork uow;

        public CreateEmptyMatchPerformanceCommandHandler(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public Task<Domain.MatchPerformance> Handle(CreateEmptyMatchPerformanceCommand request, CancellationToken cancellationToken)
        {

            Domain.MatchPerformance mp = new() { MatchID = request.MatchID, PlayerID = request.PlayerID, FinalScore = 0, MMRDelta = 0, WonMatch = false };

            if(uow.MatchPerformanceRepository.Query().Where((mp) => mp.MatchID == request.MatchID && request.PlayerID==mp.PlayerID).FirstOrDefault()!=null)
                return Task.FromResult<Domain.MatchPerformance>(null);
            if (uow.PlayerRepository.GetById(request.PlayerID).AccountDeleted)
                return Task.FromResult<Domain.MatchPerformance>(null);
            if (uow.MatchPerformanceRepository.Query().Where((mp)=>mp.MatchID == request.MatchID).Count() >= 2 || uow.PlayerRepository.GetById(request.PlayerID)==null || uow.MatchRepository.GetById(request.MatchID)==null) 
                return Task.FromResult<Domain.MatchPerformance>(null);

            uow.MatchPerformanceRepository.Add(mp);
            uow.SaveChanges();
            return Task.FromResult(mp);
        }
    }
}
