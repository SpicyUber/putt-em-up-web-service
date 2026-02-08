using Application.DTOs;
using Infrastructure.Persistence.UnitOfWork;
using MediatR;

namespace Application.Player.Queries
{
    internal class FindAccountByIdQueryHandler : IRequestHandler<FindAccountByIdQuery, Account>
    {
        private readonly IUnitOfWork uow;

        public FindAccountByIdQueryHandler(IUnitOfWork uow) { this.uow = uow; }
        public Task<Account> Handle(FindAccountByIdQuery request, CancellationToken cancellationToken)
        {
           Domain.Player player = uow.PlayerRepository.GetById(request.Id);
            if (player == null || player.AccountDeleted) { return Task.FromResult<Account>(null); }
            int matchmakingRanking = uow.PlayerRepository.GetTotalMatchmakingRanking(player.Id);
            return Task.FromResult(new Account(player,matchmakingRanking));
        }
    }
}
