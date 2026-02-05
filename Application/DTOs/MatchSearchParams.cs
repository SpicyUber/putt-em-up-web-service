
namespace Application.DTOs
{
    public class MatchSearchParams
    {

        public long PlayerID { get; set; }
        public DateTime StartDate {get; set;}
        

        public SearchMode Mode { get;set;}

        public int? PageSize { get; set; }

        public int? PageNumber { get; set; }
    }
}
