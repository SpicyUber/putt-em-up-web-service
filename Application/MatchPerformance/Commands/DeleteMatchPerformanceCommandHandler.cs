using Infrastructure.Persistence.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MatchPerformance.Commands
{
    public class DeleteMatchPerformanceCommandHandler : IRequestHandler<DeleteMatchPerformanceCommand,bool>
    {
        private readonly IUnitOfWork uow;
        public DeleteMatchPerformanceCommandHandler(IUnitOfWork uow) { this.uow = uow; }

        public Task<bool> Handle(DeleteMatchPerformanceCommand request, CancellationToken cancellationToken)
        {
            Domain.MatchPerformance matchPerformance =  uow.MatchPerformanceRepository.Query().FirstOrDefault(mp => mp.PlayerID == request.PlayerID && mp.MatchID == request.MatchID);
            if(matchPerformance == null) 
            return Task.FromResult(false);

            uow.MatchPerformanceRepository.Delete(matchPerformance);
            uow.SaveChanges();
            return Task.FromResult(true);
        }
    }
}
