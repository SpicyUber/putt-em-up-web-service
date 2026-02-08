using Domain;

namespace Application.DTOs
{
    public class ProfilePreview
    {
        public long PlayerID { get; set; }
        public string DisplayName { get; set; }
        public byte[] Avatar { get; set; }
        public ProfilePreview(Domain.Player player, byte[] avatar)
        {
            PlayerID=player.Id;
            DisplayName=player.DisplayName;

            if (avatar != null && avatar.Length>0) 
            Avatar = avatar;
        }
    }
}
