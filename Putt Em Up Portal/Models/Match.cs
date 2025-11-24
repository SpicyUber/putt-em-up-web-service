namespace Putt_Em_Up_Portal.Models
{
    public class Match
    {
        public int MatchID { get; set; }
        public DateTime StartDate { get; set; }

        public bool Cancelled { get; set; }

        public static IEnumerable<Match> GetSampleList()
        {
            LinkedList<Match> list = new LinkedList<Match>();
            list.AddLast(new Match() { MatchID = 0, StartDate = new DateTime(2022, 6, 3) });
            list.AddLast(new Match() { MatchID = 1, StartDate = new DateTime(2022, 7, 23) });
            list.AddLast(new Match() { MatchID = 2, StartDate = new DateTime(2023, 2, 1) });
            return list;
        }
    }
}
