using Domain;
 

namespace Application.DTOs
{
    public class MatchPerformancePreview
    {
        public ProfilePreview Player { get; set; }
        public bool WonMatch { get; set; }
        public int FinalScore { get; set; }
       

        public MatchPerformancePreview(Domain.MatchPerformance matchPerformance, Domain.Player player) {

            Player = new ProfilePreview(player);

            WonMatch = matchPerformance.WonMatch;
            FinalScore = matchPerformance.FinalScore;
        }
    }
}
