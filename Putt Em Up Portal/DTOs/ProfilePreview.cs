using Domain;

namespace Putt_Em_Up_Portal.DTOs
{
    public class ProfilePreview
    {
        public long PlayerID { get; set; }
        public string DisplayName { get; set; }
        public byte[] Avatar { get; set; }
        public ProfilePreview(Player player)
        {
            PlayerID=player.PlayerID;
            DisplayName=player.DisplayName;

            if (player.AvatarFilePath == null || player.AvatarFilePath.Length == 0 || this.PlayerID < 0 || player.Username.Length == 0) { player.AvatarFilePath = ""; }
            this.Avatar = File.ReadAllBytes($"." + Path.DirectorySeparatorChar + "ProfilePictures" + Path.DirectorySeparatorChar + player.AvatarFilePath + ".png");

        }
    }
}
