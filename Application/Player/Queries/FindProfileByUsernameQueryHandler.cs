using Application.DTOs;
using Infrastructure.Persistence.AvatarProvider;
using Infrastructure.Persistence.UnitOfWork;
using MediatR;

namespace Application.Player.Queries
{
    public class FindProfileByUsernameQueryHandler : IRequestHandler<FindProfileByUsernameQuery, Profile>
    {
        private readonly IUnitOfWork uow;
        private readonly IAvatarProvider ap;
        public FindProfileByUsernameQueryHandler(IUnitOfWork uow,IAvatarProvider ap) {
        this.uow = uow; this.ap = ap;
        }
        public Task<Profile> Handle(FindProfileByUsernameQuery request, CancellationToken cancellationToken)
        {
           Domain.Player player =uow.PlayerRepository.GetByUsername(request.Username);
            if (player == null || player.AccountDeleted)  return Task.FromResult<Profile>(null);
            return Task.FromResult(new Profile(player, ap.GetAvatar(player.AvatarFilePath)));
        }
    }
}
