using Infrastructure.Persistence.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Player.Commands
{
    public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand,bool>
    {
        private readonly IUnitOfWork uow;

        public DeleteAccountCommandHandler(IUnitOfWork uow) { this.uow = uow; }
        public Task<bool> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
             
            
                Domain.Player p = uow.PlayerRepository.GetById(request.Id);

                if(p==null)return Task.FromResult(false);
                p.AccountDeleted = true;
                
                uow.PlayerRepository.Update(p);
                uow.SaveChanges();
            
            return Task.FromResult(true);
        }
    }
}
