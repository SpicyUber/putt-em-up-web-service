using Infrastructure.Persistence.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Message.Queries
{
    public class FindMessageQueryHandler : IRequestHandler<FindMessageQuery, Domain.Message>
    {
        private readonly IUnitOfWork uow;

        public FindMessageQueryHandler(IUnitOfWork uow) {this.uow=uow;}
        public Task<Domain.Message> Handle(FindMessageQuery request, CancellationToken cancellationToken)
        {
           Domain.Message m = uow.MessageRepository.Query().FirstOrDefault((m) => m.FromPlayerID == request.FromPlayerID && request.SentTimestamp == m.SentTimestamp && request.ToPlayerID == m.ToPlayerID );
          return Task.FromResult(m);
        }
    }
}
