using SiteAnalysisCS;
namespace SiteAnalysisCSTest;

[TestClass]
public class ContentProcessorTests
{
    [TestMethod]
    public void TestProcessContent()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        // Arrange
        var processor = new ContentProcessor();
        string content = "Это \"пример\" текста для тестирования обработки контента.";
        // Act
        processor.ProcessContent(content);

        // 2nd run to check accumulation
        processor.ProcessContent(content);
        var wordCounts = processor.GetWordCounts();
        var exampleWordData = wordCounts.Find((wd) => wd.Word == "пример");
        Assert.IsNotNull(exampleWordData);
        Assert.AreEqual(2, exampleWordData.Count);

        // 3rd run to check accumulation
        processor.ProcessContent("Еше один пример");
        wordCounts = processor.GetWordCounts();
        exampleWordData = wordCounts.Find((wd) => wd.Word == "пример");
        Assert.IsNotNull(exampleWordData);
        Assert.AreEqual(3, exampleWordData.Count);
    }
}
