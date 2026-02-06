using Infrastructure.Persistence.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Match.Commands
{
    public class DeleteMatchCommandHandler : IRequestHandler<DeleteMatchCommand,bool>
    {
        private readonly IUnitOfWork uow;

        public DeleteMatchCommandHandler(IUnitOfWork uow) { this.uow = uow; }

        public Task<bool> Handle(DeleteMatchCommand request, CancellationToken cancellationToken)
        {
            Domain.Match match = uow.MatchRepository.GetById(request.Id);
            if (match == null) return Task.FromResult(false);
            uow.MatchRepository.Delete(match);
            uow.SaveChanges();
            return Task.FromResult(true);

        }
    }
}
