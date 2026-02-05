using Domain;
namespace Application.DTOs
{
    public class Account
    {
        public long PlayerID { get; set; }

        public string Username { get; set; }
        public int MatchmakingRanking { get; set; }

        public Account(Domain.Player p)
        {
            PlayerID = p.PlayerID;
            Username = p.Username;
            MatchmakingRanking = p.MatchmakingRanking;
        }
        
    }
}
