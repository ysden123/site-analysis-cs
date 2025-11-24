using Serilog;

namespace SiteAnalysisCS
{
    public class StartPageWebScraper : IWebScraper
    {
        private static readonly ILogger _logger = Log.ForContext<StartPageWebScraper>();

        public StartPageWebScraper()
        {
        }

        public string Name()
        {
            return "StartPage";
        }

        public string Url()
        {
            return "https://news.startpage.co.il/russian/";
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
                    var articleNodes = document.DocumentNode.SelectNodes("//ul[@class='menu']");
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
                    _logger.Error(ex, "Error during scraping StartPage");
                    throw;
                }
            });
        }
    }

}
