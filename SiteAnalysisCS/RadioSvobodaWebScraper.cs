using Serilog;

namespace SiteAnalysisCS
{
    public class RadioSvobodaWebScraper : IWebScraper
    {
        private static readonly ILogger _logger = Log.ForContext<RadioSvobodaWebScraper>();

        public string Name()
        {
            return "Radio Svoboda";
        }

        public string Url()
        {
            return "https://www.svoboda.org/news";
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
                    var articleNodes = document.DocumentNode.SelectNodes("//h4[@class='media-block__title media-block__title--size-3 media-block__title--25']");
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
                    _logger.Error(ex, "Error during scraping Radio Svoboda");
                    throw;
                }
            });
        }
    }
}
