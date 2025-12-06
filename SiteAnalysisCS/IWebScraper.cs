namespace SiteAnalysisCS
{
    /// <summary>
    /// Defines the contract for a web scraper that extracts data from a specified URL.
    /// </summary>
    /// <remarks>Implementations of this interface should provide logic to retrieve and process web content
    /// from the given URL. The interface exposes properties for the scraper's name and target URL, allowing
    /// configuration and identification of different scraper instances.</remarks>
    public interface IWebScraper
    {
        /// <summary>
        /// Asynchronously extracts word data from the specified web page URL.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="WordData"/>
        /// objects extracted from the web page. The list will be empty if no word data is found.</returns>
        Task<List<WordData>> Scrap();

        /// <summary>
        /// Gets the name associated with the scraper.
        /// </summary>
        string Name();

        /// <summary>
        /// Gets the URL associated with the scraper.
        /// </summary>
        string Url();
    }
}
