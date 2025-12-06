using SiteAnalysisCS;

namespace SiteAnalysisCSTest;

[TestClass]
public class RadioSvobodaWebScraperTests
{
    [TestMethod]
    public void TestCtor()
    {
        IWebScraper scraper = new RadioSvobodaWebScraper();
        Assert.AreEqual("Radio Svoboda", scraper.Name());
    }

    [TestMethod]
    public async Task TestScrap()
    {
        try
        {
            IWebScraper scraper = new RadioSvobodaWebScraper();
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
