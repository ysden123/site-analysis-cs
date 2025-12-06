using Serilog;

namespace SiteAnalysisCS
{
    public class IsrageoWebScraper : IWebScraper
    {
        private static readonly ILogger _logger = Log.ForContext<IsrageoWebScraper>();

        public string Name()
        {
            return "Isrageo";
        }

        public string Url()
        {
            return "https://www.isrageo.com/";
        }

        public Task<List<WordData>> Scrap()
        {
            return Task.Run(async () =>
            {
                try
                {
                    var contentProcessor = new ContentProcessor();
                    var htmlWeb = new HtmlAgilityPack.HtmlWeb();
                    var document = await htmlWeb.LoadFromWebAsync(Url());
                    var articleNodes = document.DocumentNode.SelectNodes("//div[@class='widget widget_recent_entries']/ul/li/a");
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
