using BLBConcordance.Core;
using EnsureThat;
using OpenQA.Selenium;

namespace BLBConcordance.BlueLetterBible
{
    public sealed class SearchOperation : IWebOperation<string, string>
    {
        private readonly By input;
        public SearchOperation(By input)
        {
            this.input = EnsureArg.IsNotNull(input, nameof(input));
        }

        public string? Operate(IWebCrawler webCrawler, string context)
        {
            var result = webCrawler.FindElements(this.input);
            return result[0]?.Text;
        }

        public object Operate(IWebCrawler webCrawler, dynamic input)
        {
            return this.Operate(webCrawler, input);
        }
    }
}