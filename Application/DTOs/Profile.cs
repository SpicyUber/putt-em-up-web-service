using Domain;

namespace Application.DTOs
{
    public class Profile
    {

        public Profile(Domain.Player p) {
            PlayerID = p.PlayerID;
            DisplayName = p.DisplayName;
            Description = p.Description;
            if (p.AvatarFilePath == null || p.AvatarFilePath.Length == 0 || PlayerID < 0 || p.Username.Length==0) { p.AvatarFilePath = ""; }
            Avatar = File.ReadAllBytes($"."+Path.DirectorySeparatorChar+"ProfilePictures"+Path.DirectorySeparatorChar+p.AvatarFilePath+".png");
        }
        public long PlayerID { get; set; }
        public string DisplayName { get; set; }

        public string Description { get; set; }

        public byte[] Avatar { get; set; }
    }
}
