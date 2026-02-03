namespace Putt_Em_Up_Portal.DTOs
{
    public class LeaderboardPage
    {
        public LeaderboardPage(Profile[] profiles, int totalPages)
        {
            Profiles = profiles;
            TotalPages = totalPages;
        }

        public Profile[] Profiles {get; set;}
        public int TotalPages { get; set; }
    }
}
