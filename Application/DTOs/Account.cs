using Domain;
namespace Application.DTOs
{
    public class Account
    {
        public long PlayerID { get; set; }

        public string Username { get; set; }
        public int MatchmakingRanking { get; set; }

        public Account(Domain.Player p,int matchmakingRanking)
        {
            PlayerID = p.Id;
            Username = p.UserName;
            this.MatchmakingRanking = matchmakingRanking;
        }
        
    }
}
