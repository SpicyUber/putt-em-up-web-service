using Domain;
using Infrastructure.Persistence.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Message.Commands
{
    public class EditMessageCommandHandler : IRequestHandler<EditMessageCommand, Domain.Message>
    {
        private readonly IUnitOfWork uow;

        public EditMessageCommandHandler(IUnitOfWork uow) {this.uow=uow;}
        public Task<Domain.Message> Handle(EditMessageCommand request, CancellationToken cancellationToken)
        {
            Domain.Message message = uow.MessageRepository.Query().FirstOrDefault((m) =>  m.FromPlayerID == request.FromPlayerID && m.SentTimestamp == request.SentTimestamp && m.ToPlayerID == request.ToPlayerID);
            if (message == null) { return Task.FromResult<Domain.Message>(null); }
            if (request.EditParams.Content != null) message.Content = request.EditParams.Content;
            if (request.EditParams.Reported != null) message.Reported = (bool)request.EditParams.Reported;
            uow.MessageRepository.Update(message);
            uow.SaveChanges();
            return Task.FromResult(message);
        }
    }
}
