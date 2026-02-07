using Domain;
using Infrastructure.Persistence.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Message.Queries
{
    public class GetAllRecentMessagesQueryHandler : IRequestHandler<GetAllRecentMessagesQuery, List<Domain.Message>>
    {
        private readonly IUnitOfWork uow;

        public GetAllRecentMessagesQueryHandler(IUnitOfWork uow) {this.uow=uow;}
        public Task<List<Domain.Message>> Handle(GetAllRecentMessagesQuery request, CancellationToken cancellationToken)
        {
           List<Domain.Message> result =
             uow.MessageRepository.Query()
            .Where<Domain.Message>((Domain.Message m) =>  (m.FromPlayerID == request.PlayerID || m.ToPlayerID == request.PlayerID) && m.SentTimestamp.AddDays(7.0) > DateTime.Now)
            .OrderByDescending((Domain.Message m1) => m1.SentTimestamp).ToList();

            if (result.Count == 0) return Task.FromResult(new List<Domain.Message>());
            return Task.FromResult(result);
        }
    }
}
