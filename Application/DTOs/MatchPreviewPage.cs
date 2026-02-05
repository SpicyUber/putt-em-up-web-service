namespace Application.DTOs
{
    public class MatchPreviewPage
    {
        public MatchPreviewPage(MatchPreview[] matches, int totalPages)
        {
            Matches = matches;
            TotalPages = totalPages;
        }

        public MatchPreview[] Matches { get; set; }
        public int TotalPages { get; set; }
    }
}
