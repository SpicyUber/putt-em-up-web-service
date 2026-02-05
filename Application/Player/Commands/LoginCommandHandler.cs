using Application.DTOs;
using Infrastructure.Persistence.UnitOfWork;
using MediatR;
 

namespace Application.Player.Commands
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginAnswer>
    {
        private readonly IUnitOfWork uow;

        public LoginCommandHandler(IUnitOfWork uow) { this.uow = uow; }
        public Task<LoginAnswer> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
           Domain.Player? player =uow.PlayerRepository.Query().FirstOrDefault(
                
            (Domain.Player p) => p.Username == request.Username && p.Password == request.Password
            
            );

            return (player == null) ?
            Task.FromResult<LoginAnswer>(null) : Task.FromResult(new LoginAnswer(player));
            
        }
    }
}
