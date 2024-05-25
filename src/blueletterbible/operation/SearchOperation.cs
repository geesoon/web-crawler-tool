using BLBConcordance.BlueLetterBible.Model;
using BLBConcordance.Core;
using EnsureThat;
using OpenQA.Selenium;

namespace BLBConcordance.BlueLetterBible
{
    public sealed class SearchOperation : WebOperationBase<string, string>
    {
        private readonly By by;
        private readonly string criteria;
        private readonly BibleTranslation translation;
        public SearchOperation(By by, string criteria, BibleTranslation translation)
        {
            this.by = EnsureArg.IsNotNull(by, nameof(by));
            this.criteria = EnsureArg.IsNotNull(criteria, nameof(criteria));
            this.translation = translation;
        }

        public override string Operate(IWebCrawler webCrawler, string context)
        {
            webCrawler.BrowseUrl($"https://www.blueletterbible.org/search/search.cfm?Criteria={this.criteria}&t={this.translation}");
            var result = webCrawler.FindElements(this.by).ToList();
            if (result.Count > 0)
            {
                return result[0].Text;
            }
            return string.Empty;
        }
    }
}