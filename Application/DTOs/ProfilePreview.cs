using Domain;

namespace Application.DTOs
{
    public class ProfilePreview
    {
        public long PlayerID { get; set; }
        public string DisplayName { get; set; }
        public byte[] Avatar { get; set; }
        public ProfilePreview(Domain.Player player)
        {
            PlayerID=player.PlayerID;
            DisplayName=player.DisplayName;

            if (player.AvatarFilePath == null || player.AvatarFilePath.Length == 0 || PlayerID < 0 || player.Username.Length == 0) { player.AvatarFilePath = ""; }
            Avatar = File.ReadAllBytes($"." + Path.DirectorySeparatorChar + "ProfilePictures" + Path.DirectorySeparatorChar + player.AvatarFilePath + ".png");

        }
    }
}
