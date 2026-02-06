using Application.DTOs;
using Infrastructure.Persistence.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Player.Commands
{
    public class EditAccountCommandHandler : IRequestHandler<EditAccountCommand, Account>
    {
        private readonly IUnitOfWork uow;

        public EditAccountCommandHandler(IUnitOfWork uow) { this.uow = uow; }
        public Task<Account> Handle(EditAccountCommand request, CancellationToken cancellationToken)
        {
            Domain.Player player = uow.PlayerRepository.GetById(request.Id);
            if (player == null || player.AccountDeleted || (request.AccountParams.Username!=null && uow.PlayerRepository.GetByUsername(request.AccountParams.Username)!=null))
            return Task.FromResult<Account>(null);

            if (request.AccountParams.Username != null) player.Username = request.AccountParams.Username;
            if (request.AccountParams.Password != null) player.Password = request.AccountParams.Password;
            int matchmakingRanking = uow.MatchPerformanceRepository.Query().Where((mp) => mp.PlayerID == player.PlayerID).Sum(mp => mp.MMRDelta);
            uow.PlayerRepository.Update(player);
            uow.SaveChanges();
            return Task.FromResult(new Account(player, matchmakingRanking));
        }
    }
}
