using Application.DTOs;
using Infrastructure.Persistence.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Identity;
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
        private readonly IPasswordHasher<Domain.Player> hasher;

        public EditAccountCommandHandler(IUnitOfWork uow, IPasswordHasher<Domain.Player> hasher) { this.uow = uow; this.hasher = hasher; }
        public Task<Account> Handle(EditAccountCommand request, CancellationToken cancellationToken)
        {
            Domain.Player player = uow.PlayerRepository.GetById(request.Id);
            if (player == null || player.AccountDeleted || (request.AccountParams.Username!=null && uow.PlayerRepository.GetByUsername(request.AccountParams.Username)!=null))
            return Task.FromResult<Account>(null);

            if (request.AccountParams.Username != null) player.UserName = request.AccountParams.Username;
            if (request.AccountParams.Password != null) player.PasswordHash = hasher.HashPassword(player,request.AccountParams.Password);
            
            
            int matchmakingRanking = uow.PlayerRepository.GetTotalMatchmakingRanking(request.Id);
            uow.PlayerRepository.Update(player);
            uow.SaveChanges();
            return Task.FromResult(new Account(player, matchmakingRanking));
        }
    }
}
