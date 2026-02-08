using Application.DTOs;
using Application.Services;
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
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand,LoginAnswer>
    {
        private readonly IUnitOfWork uow;
        private readonly UserManager<Domain.Player> userManager;
        private readonly IPasswordHasher<Domain.Player> hasher;
        private readonly JwtService jwt;
       public RegisterCommandHandler(IUnitOfWork uow, UserManager<Domain.Player> userManager, IPasswordHasher<Domain.Player> hasher, JwtService jwt) 
        { this.uow = uow; this.userManager = userManager; this.hasher = hasher; this.jwt = jwt; }

        public async Task<LoginAnswer> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if ( await userManager.FindByNameAsync(request.Username) != null)
                return null;
            Domain.Player p = new Domain.Player();
            p.UserName = request.Username;
            

            p.DisplayName = p.UserName;
            p.Description = $"Hi, I'm {p.DisplayName}";
            p.AccountDeleted = false;
            p.AvatarFilePath = "";
            p.PasswordHash = hasher.HashPassword(p, request.Password);
            await userManager.CreateAsync(p);
            await userManager.AddToRoleAsync(p,"user");
            uow.SaveChanges();
            return new LoginAnswer(p,await jwt.GenerateToken(p),false);
           
        }
    }
}
