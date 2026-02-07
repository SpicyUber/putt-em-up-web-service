using Infrastructure.Persistence.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MatchPerformance.Commands
{
    public class EditMatchPerformanceCommandHandler : IRequestHandler<EditMatchPerformanceCommand,Domain.MatchPerformance>
    {
        private readonly IUnitOfWork uow;
        public EditMatchPerformanceCommandHandler(IUnitOfWork uow) { this.uow = uow; }

        public Task<Domain.MatchPerformance> Handle(EditMatchPerformanceCommand request, CancellationToken cancellationToken)
        {
            Domain.MatchPerformance matchPerformance = uow.MatchPerformanceRepository.Query().FirstOrDefault(mp => mp.PlayerID == request.PlayerID && mp.MatchID == request.MatchID);

            if (matchPerformance == null) { return Task.FromResult<Domain.MatchPerformance>(null); }

            matchPerformance.MMRDelta = request.EditParams.MMRDelta;
            matchPerformance.WonMatch = request.EditParams.WonMatch;
            matchPerformance.FinalScore = request.EditParams.FinalScore;

            uow.MatchPerformanceRepository.Update(matchPerformance);

            uow.SaveChanges();

            return Task.FromResult(matchPerformance);
        }
    }
}
