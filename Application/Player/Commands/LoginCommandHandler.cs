using Application.DTOs;
using Application.Services;
using Infrastructure.Persistence.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Identity;


namespace Application.Player.Commands
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginAnswer>
    {
        private readonly IUnitOfWork uow;
        private readonly UserManager<Domain.Player> userManager;
        private readonly JwtService jwt;

       public LoginCommandHandler(IUnitOfWork uow, UserManager<Domain.Player> userManager, JwtService jwt) { this.uow = uow; this.userManager = userManager; this.jwt = jwt; }
        public async Task<LoginAnswer> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
          Domain.Player player = await userManager.FindByNameAsync(request.Username);

            if (player == null || player.AccountDeleted || !await userManager.CheckPasswordAsync(player,request.Password)) return null;
            
            return new LoginAnswer(player,await jwt.GenerateToken(player),(await userManager.GetRolesAsync(player)).Contains("admin"));
            
        }
    }
}
