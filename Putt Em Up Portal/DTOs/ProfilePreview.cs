using Putt_Em_Up_Portal.Models;

namespace Putt_Em_Up_Portal.DTOs
{
    public class ProfilePreview
    {
        public long PlayerID { get; set; }
        public string DisplayName { get; set; }
        public string AvatarFilePath { get; set; }
        public ProfilePreview(Player player)
        {
            PlayerID=player.PlayerID;
            DisplayName=player.DisplayName;
            AvatarFilePath=player.AvatarFilePath;


        }
    }
}
