using Putt_Em_Up_Portal.Models;

namespace Putt_Em_Up_Portal.DTOs
{
    public class MatchPreview
    {
        public long MatchID { get; set; }
        public DateTime StartDate { get; set; }



        public MatchPerformancePreview[] MatchPerformances { get; set; }

        public MatchPreview(Match match,Player player1, Player player2, MatchPerformance matchPerformancePlayer1, MatchPerformance matchPerformancePlayer2)
        {
            MatchID = match.MatchID;
            StartDate = match.StartDate;
            MatchPerformances = new MatchPerformancePreview[2];
            MatchPerformances[0] = new(matchPerformancePlayer1, player1);
            MatchPerformances[1] = new(matchPerformancePlayer2, player2);
        }
    }
}
