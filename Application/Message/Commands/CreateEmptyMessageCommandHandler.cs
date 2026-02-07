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
    public class CreateEmptyMessageCommandHandler : IRequestHandler<CreateEmptyMessageCommand, Domain.Message>
    {
        private readonly IUnitOfWork uow;

        public CreateEmptyMessageCommandHandler(IUnitOfWork uow) {this.uow=uow;}
        public Task<Domain.Message> Handle(CreateEmptyMessageCommand request, CancellationToken cancellationToken)
        {
            if (uow.PlayerRepository.GetById(request.FromPlayerID) == null || uow.PlayerRepository.GetById(request.ToPlayerID) == null) return Task.FromResult<Domain.Message>(null);
           Domain.Message message = new() { FromPlayerID = request.FromPlayerID, ToPlayerID = request.ToPlayerID, Content = "Message is being processed...", Reported = false, SentTimestamp = DateTime.Now };
            uow.MessageRepository.Add(message);
            uow.SaveChanges();
            return Task.FromResult(message);
        }
    }
}
