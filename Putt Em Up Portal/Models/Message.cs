namespace Putt_Em_Up_Portal.Models
{
    public class Message
    {
        public long FromPlayerID { get; set; }
        public long ToPlayerID { get; set; }
        public DateTime SentTimestamp { get; set; }
        public bool Reported { get; set; }
        public string Content { get; set; }
    }
}
