namespace Core.Entities
{
    public class SearchPreviewData
    {
        public string? Title { get; set; }
        public string? Year { get; set; }
        public string? imdbID { get; set; }
        public string? Type { get; set; }
        public string? Poster { get; set; }
    }

    public class SearchPreviewResponse
    {
        public SearchPreviewData[]? Search { get; set; }
        public string? totalResults { get; set; }
        public string? Response { get; set; }
        public bool IsSuccess => Response?.ToLower() == "true";
    }
}
