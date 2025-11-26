using System.ComponentModel;

namespace Putt_Em_Up_Portal.Models
{
    public class Player
    {
        public long PlayerID { get; set; }
        public string Username { get; set; }
        
        public string Password { get; set; }

        public int MatchmakingRanking { get; set; }

        public bool AccountDeleted { get; set; }

        public string DisplayName { get; set; } 

        public string Description { get; set; }

        public string AvatarFilePath { get; set; }


    }
}
