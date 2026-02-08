using Domain;

namespace Application.DTOs
{
    public class Profile
    {

        public Profile(Domain.Player p, byte[] avatar) {
            PlayerID = p.Id;
            DisplayName = p.DisplayName;
            Description = p.Description;
            if(avatar != null && avatar.Length>0)
            Avatar = avatar;
        }
        public long PlayerID { get; set; }
        public string DisplayName { get; set; }

        public string Description { get; set; }

        public byte[] Avatar { get; set; }
    }
}
