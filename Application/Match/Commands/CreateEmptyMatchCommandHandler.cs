using Infrastructure.Persistence.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Match.Commands
{
    public class CreateEmptyMatchCommandHandler : IRequestHandler<CreateEmptyMatchCommand, Domain.Match>
    {
        private readonly IUnitOfWork uow;
        public CreateEmptyMatchCommandHandler(IUnitOfWork uow) { this.uow = uow; }

        public Task<Domain.Match> Handle(CreateEmptyMatchCommand request, CancellationToken cancellationToken)
        {

            Domain.Match match = new() { Cancelled = true, StartDate = DateTime.Now.Date };
            uow.MatchRepository.Add(match);
            uow.SaveChanges();
             

            return Task.FromResult(match);
        }
    }
}
