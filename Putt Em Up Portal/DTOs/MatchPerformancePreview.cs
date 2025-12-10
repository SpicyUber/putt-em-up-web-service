using Putt_Em_Up_Portal.Models;

namespace Putt_Em_Up_Portal.DTOs
{
    public class MatchPerformancePreview
    {
        public ProfilePreview Player { get; set; }
        public bool WonMatch { get; set; }
        public int FinalScore { get; set; }
       

        public MatchPerformancePreview(MatchPerformance matchPerformance, Player player) { 

        this.Player = new ProfilePreview(player);
          
            this.WonMatch = matchPerformance.WonMatch;
            this.FinalScore = matchPerformance.FinalScore;
        }
    }
}
