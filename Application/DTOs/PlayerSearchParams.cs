namespace Application.DTOs
{
    public class PlayerSearchParams
    {
        public string? UsernameStartsWith { get; set; }
        public bool DescendingRanking { get; set; }

        public int? PageSize { get; set; }

        public int? PageNumber { get; set; }
    }
}
