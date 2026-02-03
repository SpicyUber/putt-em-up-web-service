using Domain;

namespace Putt_Em_Up_Portal.DTOs
{
    public class Profile
    {

        public Profile(Player p) {
            this.PlayerID = p.PlayerID;
            this.DisplayName = p.DisplayName;
            this.Description = p.Description;
            if (p.AvatarFilePath == null || p.AvatarFilePath.Length == 0 || this.PlayerID<0 || p.Username.Length==0) { p.AvatarFilePath = ""; }
            this.Avatar = File.ReadAllBytes($"."+Path.DirectorySeparatorChar+"ProfilePictures"+Path.DirectorySeparatorChar+p.AvatarFilePath+".png");
        }
        public long PlayerID { get; set; }
        public string DisplayName { get; set; }

        public string Description { get; set; }

        public byte[] Avatar { get; set; }
    }
}
