using Application.DTOs;
using Infrastructure.Persistence.UnitOfWork;
using MediatR;

namespace Application.Player.Queries
{
    internal class GetAccountQueryHandler : IRequestHandler<GetAccountQuery, Account>
    {
        private readonly IUnitOfWork uow;

        public GetAccountQueryHandler(IUnitOfWork uow) { this.uow = uow; }
        public Task<Account> Handle(GetAccountQuery request, CancellationToken cancellationToken)
        {
           Domain.Player player = uow.PlayerRepository.GetById(request.Id);


            return (player==null)?
                Task.FromResult<Account>(null):Task.FromResult(new Account(player));
        }
    }
}
