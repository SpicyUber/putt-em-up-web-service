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
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand,LoginAnswer>
    {
        private readonly IUnitOfWork uow;
       public RegisterCommandHandler(IUnitOfWork uow)
        { this.uow = uow; }

        public Task<LoginAnswer> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if (uow.PlayerRepository.GetByUsername(request.Username) != null)
                return Task.FromResult<LoginAnswer>(null);
            Domain.Player p = new Domain.Player();
            p.Username = request.Username;
            p.Password = request.Password;

            p.DisplayName = p.Username;
            p.Description = $"Hi, I'm {p.DisplayName}";
            p.AccountDeleted = false;
            p.AvatarFilePath = "";
            uow.PlayerRepository.Add(p);
            uow.SaveChanges();
            return Task.FromResult(new LoginAnswer(p));
        }
    }
}
