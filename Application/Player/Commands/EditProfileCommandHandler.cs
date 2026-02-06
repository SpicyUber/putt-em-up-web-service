using Application.DTOs;
using Infrastructure.Persistence.AvatarProvider;
using Infrastructure.Persistence.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Player.Commands
{
    public class EditProfileCommandHandler : IRequestHandler<EditProfileCommand, Profile>
    {
        private readonly IUnitOfWork uow;
        private readonly IAvatarProvider ap;

        public EditProfileCommandHandler(IUnitOfWork uow, IAvatarProvider ap)
        { this.uow = uow; this.ap = ap; }

        public Task<Profile> Handle(EditProfileCommand request, CancellationToken cancellationToken)
        {

            Domain.Player oldProfile = uow.PlayerRepository.GetByUsername(request.Username);
            ProfileEditParams profileChanges = request.Profile;
            if (oldProfile != null && oldProfile.AccountDeleted == false)
            {
                oldProfile.DisplayName = profileChanges.DisplayName;
                oldProfile.Description = profileChanges.Description;

                if (profileChanges.AvatarInBase64 != null && profileChanges.AvatarInBase64.Length > 0 )
                    oldProfile.AvatarFilePath = ap.CreateAvatar(profileChanges.AvatarInBase64, request.Username);
               
                uow.PlayerRepository.Update(oldProfile);
                uow.SaveChanges();

                return Task.FromResult(new Profile(oldProfile, ap.GetAvatar(oldProfile.AvatarFilePath)));

            }
            else return Task.FromResult<Profile>(null);

        }


    }
}
