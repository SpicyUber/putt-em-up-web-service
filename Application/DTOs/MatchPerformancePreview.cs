using Domain;
 

namespace Application.DTOs
{
    public class MatchPerformancePreview
    {
        public ProfilePreview Player { get; set; }
        public bool WonMatch { get; set; }
        public int FinalScore { get; set; }
       

        public MatchPerformancePreview(Domain.MatchPerformance matchPerformance, Domain.Player player, byte[] playerAvatar) {

            Player = new ProfilePreview(player, playerAvatar);

            WonMatch = matchPerformance.WonMatch;
            FinalScore = matchPerformance.FinalScore;
        }
    }
}
