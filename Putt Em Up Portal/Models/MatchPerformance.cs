namespace Putt_Em_Up_Portal.Models
{
    public class MatchPerformance
    {
        public long PlayerID { get; set; }
        public long MatchID { get; set; }

        public bool WonMatch {get; set;}
        public int FinalScore { get; set;}  
        public int MMRDelta { get; set;}


        public static IEnumerable<MatchPerformance> GetSampleList()
        {
            LinkedList<MatchPerformance> list = new LinkedList<MatchPerformance>();
            list.AddLast(new MatchPerformance() { MatchID = 0, PlayerID = 0 });
            list.AddLast(new MatchPerformance() { MatchID = 0, PlayerID = 1 });
            list.AddLast(new MatchPerformance() { MatchID = 1, PlayerID = 1 });
            list.AddLast(new MatchPerformance() { MatchID = 1, PlayerID = 0 });
            list.AddLast(new MatchPerformance() { MatchID = 2, PlayerID = 1 });
            list.AddLast(new MatchPerformance() { MatchID = 2, PlayerID = 2 });
            return list;
        }

    }
}
