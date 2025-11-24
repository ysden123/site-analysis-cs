namespace SiteAnalysisCS
{
    public record WordData
    {
        public required string Word { get; set; }
        public int Count { get; set; }
    }
}
