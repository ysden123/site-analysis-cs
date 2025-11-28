using Serilog;

namespace SiteAnalysisCS
{
    public class KnopkaWebScraper : IWebScraper
    {
        private static readonly ILogger _logger = Log.ForContext<KnopkaWebScraper>();

        public string Name()
        {
            return "Knopka";
        }
        public string Url()
        {
            return "https://news.knopka.ca/";
        }

        public Task<List<WordData>> Scrap(string url)
        {
            return Task.Run(async () =>
            {
                try
                {
                    var contentProcessor = new ContentProcessor();
                    var htmlWeb = new HtmlAgilityPack.HtmlWeb();
                    var document = await htmlWeb.LoadFromWebAsync(url);
                    var articleNodes = document.DocumentNode.SelectNodes("//div[@class='recent-headlines scroll-div']/div[@class='scrollContainer']/ul[@class='news-widget']/li");
                    if (articleNodes != null)
                    {
                        foreach (var articleNode in articleNodes)
                        {
                            contentProcessor.ProcessContent(articleNode.InnerText);
                        }
                    }

                    return contentProcessor.GetWordCounts();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, "Error during scraping Knopka");
                    throw;
                }
            });
        }
    }
}
