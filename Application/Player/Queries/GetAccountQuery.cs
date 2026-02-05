using Application.DTOs;
using Infrastructure.Persistence.UnitOfWork;
using MediatR;
 

namespace Application.Player.Queries
{
    public class GetAccountQuery : IRequest<Account>
    {
      
        public long Id { get; set; }
    }
}
