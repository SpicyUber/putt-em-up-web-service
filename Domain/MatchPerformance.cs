namespace Domain
{
    public class MatchPerformance
    {
        public long PlayerID { get; set; }
        public long MatchID { get; set; }

        public bool WonMatch {get; set;}
        public int FinalScore { get; set;}  
        public int MMRDelta { get; set;}

 

    }
}
