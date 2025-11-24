using SiteAnalysisCS;

namespace SiteAnalysisCSTest;

[TestClass]
public class StartPageWebScraperTests
{
    [TestMethod]
    public void TestCtor()
    {
        IWebScraper scraper = new StartPageWebScraper();
        Assert.AreEqual("StartPage", scraper.Name());
    }
    [TestMethod]
    public async Task TestScrap()
    {
        try
        {
            IWebScraper scraper = new StartPageWebScraper();
            var result = await scraper.Scrap(scraper.Url());
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
