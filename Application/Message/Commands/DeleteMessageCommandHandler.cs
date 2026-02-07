using Infrastructure.Persistence.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Message.Commands
{
    public class DeleteMessageCommandHandler : IRequestHandler<DeleteMessageCommand, bool>
    {
        private readonly IUnitOfWork uow;

        public DeleteMessageCommandHandler(IUnitOfWork uow) {this.uow=uow;}
        public Task<bool> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
        {
            Domain.Message m = uow.MessageRepository.Query()
            .FirstOrDefault((m) => m.FromPlayerID == request.FromPlayerID && request.SentTimestamp == m.SentTimestamp && request.ToPlayerID == m.ToPlayerID);

            if(m==null)
            return Task.FromResult(false);
           
            uow.MessageRepository.Delete(m);
            uow.SaveChanges();
            return Task.FromResult(true);
        }
    }
}
