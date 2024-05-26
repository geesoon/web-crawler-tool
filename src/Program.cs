using BLBConcordance.BlueLetterBible;
using BLBConcordance.Core.Services;
using OpenQA.Selenium.Chrome;

namespace BLBConcordance
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var workflow = new WorkFlowBase();

            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--headless");
            chromeOptions.AddArgument("--disable-gpu");

            using var blueLetterBibleWebDriver = new ChromeDriver(chromeOptions)
            {
                Url = "https://www.blueletterbible.org/"
            };
            var blueLetterBibleWebCrawler = new SeleniumWebCrawler(blueLetterBibleWebDriver);
            var blueLetterBibleWebOperationPipeline = new WebOperationPipeline(blueLetterBibleWebCrawler);
            blueLetterBibleWebOperationPipeline.AddOperation(new SearchOperation("Love", BlueLetterBible.Model.BibleTranslation.KJV));
            var jsonFileWriter = new JsonFileWriter();
            workflow
                .AddPipeline(blueLetterBibleWebOperationPipeline)
                .Execute()
                .OutputResults(jsonFileWriter);
        }
    }
}