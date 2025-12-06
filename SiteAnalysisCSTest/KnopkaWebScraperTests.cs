using SiteAnalysisCS;

namespace SiteAnalysisCSTest;

[TestClass]
public class KnopkaWebScraperTests
{
    [TestMethod]
    public async Task TestScrap()
    {
        try
        {
            IWebScraper scraper = new KnopkaWebScraper();
            var result = await scraper.Scrap();
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);

            foreach (var wordData in result)
            {
                Assert.IsFalse(string.IsNullOrEmpty(wordData.Word));
                Assert.IsGreaterThan(1, wordData.Count);
            }

            Assert.IsLessThanOrEqualTo(15, result.Count);
        }
        catch (Exception ex)
        {
            Assert.Fail($"Exception occurred during scraping: {ex.Message}");
        }
    }
}
