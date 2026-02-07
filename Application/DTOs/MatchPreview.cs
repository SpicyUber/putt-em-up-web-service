using Domain;

namespace Application.DTOs
{
    public class MatchPreview
    {
        public long MatchID { get; set; }
        public DateTime StartDate { get; set; }



        public MatchPerformancePreview[] MatchPerformances { get; set; }

        public MatchPreview(Domain.Match match, Domain.Player player1, Domain.Player player2, Domain.MatchPerformance matchPerformancePlayer1, Domain.MatchPerformance matchPerformancePlayer2, byte[] player1Avatar, byte[] player2Avatar)
        {
            MatchID = match.MatchID;
            StartDate = match.StartDate;
            MatchPerformances = new MatchPerformancePreview[2];
            MatchPerformances[0] = new(matchPerformancePlayer1, player1,player1Avatar);
            MatchPerformances[1] = new(matchPerformancePlayer2, player2, player2Avatar);
        }
    }
}
