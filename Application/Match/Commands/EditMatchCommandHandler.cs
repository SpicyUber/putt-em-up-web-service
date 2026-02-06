using Infrastructure.Persistence.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Match.Commands
{
    public class EditMatchCommandHandler : IRequestHandler<EditMatchCommand, Domain.Match>
    {
        private readonly IUnitOfWork uow;

        public EditMatchCommandHandler(IUnitOfWork uow)
        {
            this.uow = uow;
        }
        public Task<Domain.Match> Handle(EditMatchCommand request, CancellationToken cancellationToken)
        {
            Domain.Match match = uow.MatchRepository.GetById(request.Id);
            if (match == null) { return Task.FromResult<Domain.Match>(null); }
            match.Cancelled = request.Cancelled;
            uow.MatchRepository.Update(match);
            uow.SaveChanges();
            return Task.FromResult(match);

        }
    }
}
