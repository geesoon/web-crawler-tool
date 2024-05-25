using BLBConcordance.Core;
using EnsureThat;
using OpenQA.Selenium;

namespace BLBConcordance.BlueLetterBible
{
    public sealed class TextGrabOperation : IWebOperation<IEnumerable<string>, string>
    {
        private readonly By input;
        public TextGrabOperation(By input)
        {
            this.input = EnsureArg.IsNotNull(input, nameof(input));
        }

        public IEnumerable<string>? Operate(IWebCrawler webCrawler, string context)
        {
            var result = webCrawler.FindElements(this.input);
            return result.Select(x => x.Text).ToList();
        }

        public object Operate(IWebCrawler webCrawler, dynamic input)
        {
            return this.Operate(webCrawler, input);
        }
    }
}