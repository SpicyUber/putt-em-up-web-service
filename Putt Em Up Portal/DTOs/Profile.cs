using Putt_Em_Up_Portal.Models;

namespace Putt_Em_Up_Portal.DTOs
{
    public class Profile
    {

        public Profile(Player p) {
            this.PlayerID = p.PlayerID;
            this.DisplayName = p.DisplayName;
            this.Description = p.Description;
            this.AvatarFilePath = p.AvatarFilePath;
        }
        public long PlayerID { get; set; }
        public string DisplayName { get; set; }

        public string Description { get; set; }

        public string AvatarFilePath { get; set; }
    }
}
