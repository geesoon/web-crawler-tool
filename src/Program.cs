using BLBConcordance.BlueLetterBible;
using BLBConcordance.Core;
using BLBConcordance.WorkFlow;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace BLBConcordance
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            // Define the workflow
            var workflow = new FirstOccurrenceWorkFlow();

            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--headless");
            chromeOptions.AddArgument("--disable-gpu");

            using var blueLetterBibleWebDriver = new ChromeDriver(chromeOptions)
            {
                Url = "https://www.blueletterbible.org/"
            };
            var blueLetterBibleWebCrawler = new SeleniumWebCrawler(blueLetterBibleWebDriver);
            var blueLetterBibleWebOperationPipeline = new WebOperationPipeline(blueLetterBibleWebCrawler);
            blueLetterBibleWebOperationPipeline.AddOperation(new SearchOperation(By.Id("scriptureOfTheDay")));
            blueLetterBibleWebOperationPipeline.AddOperation(new TextGrabOperation(By.ClassName("tenVersesOn")));
            workflow.AddPipeline(blueLetterBibleWebOperationPipeline);

            using var bibleGatewayWebDriver = new ChromeDriver(chromeOptions)
            {
                Url = "https://www.biblegateway.com/"
            };
            var bibleGatewayWebCrawler = new SeleniumWebCrawler(bibleGatewayWebDriver);
            var bibleGatewayWebOperationPipeline = new WebOperationPipeline(bibleGatewayWebCrawler);
            bibleGatewayWebOperationPipeline.AddOperation(new SearchOperation(By.Id("verse-text")));
            workflow.AddPipeline(bibleGatewayWebOperationPipeline);

            workflow.Execute();
        }
    }
}