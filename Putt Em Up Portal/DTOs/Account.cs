using Domain;
namespace Putt_Em_Up_Portal.DTOs
{
    public class Account
    {
        public long PlayerID { get; set; }

        public string Username { get; set; }
        public int MatchmakingRanking { get; set; }

        public Account(Player p)
        {
            this.PlayerID = p.PlayerID;
            this.Username = p.Username;
            this.MatchmakingRanking = p.MatchmakingRanking;
        }
        
    }
}
