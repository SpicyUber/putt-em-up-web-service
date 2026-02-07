using Infrastructure.Persistence.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Message.Queries
{
    public class GetChatQueryHandler : IRequestHandler<GetChatQuery, List<Domain.Message>>
    {
        private readonly IUnitOfWork uow;

        public GetChatQueryHandler(IUnitOfWork uow) {this.uow=uow;}
        public Task<List<Domain.Message>> Handle(GetChatQuery request, CancellationToken cancellationToken)
        {
           List<Domain.Message> result = uow.MessageRepository.Query()
           .Where<Domain.Message>((m) => (m.FromPlayerID == request.FirstPlayerID && request.SecondPlayerID == m.ToPlayerID || m.FromPlayerID == request.SecondPlayerID && request.FirstPlayerID == m.ToPlayerID)).OrderBy((Domain.Message m1) => m1.SentTimestamp).ToList();
            if(result==null) return Task.FromResult<List<Domain.Message>>(new List<Domain.Message>());
            return Task.FromResult(result);
        }
    }
}
