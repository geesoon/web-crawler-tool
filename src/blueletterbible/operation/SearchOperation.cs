using BLBConcordance.BlueLetterBible.Model;
using BLBConcordance.Core.Interfaces;
using BLBConcordance.Core.Model;
using BLBConcordance.Core.Services;
using EnsureThat;
using OpenQA.Selenium;

namespace BLBConcordance.BlueLetterBible
{
    public sealed class SearchOperation : WebOperationBase<IEnumerable<BibleVerse>, string>
    {
        private readonly string SearchUrl = "https://www.blueletterbible.org/search/search.cfm";
        private readonly By By = By.CssSelector(".scriptureText");
        private readonly string Criteria;
        private readonly BibleTranslation Translation;
        public SearchOperation(string criteria, BibleTranslation translation)
        {
            this.Criteria = EnsureArg.IsNotNull(criteria, nameof(criteria));
            this.Translation = translation;
        }

        public override IEnumerable<BibleVerse> Operate(IWebCrawler webCrawler, string context)
        {
            webCrawler.BrowseUrl($"{this.SearchUrl}?Criteria={this.Criteria}&t={this.Translation}");
            var result = webCrawler
                .FindElements(this.By)
                .ToList();

            IEnumerable<BibleVerse> bibleVerses = [];
            foreach (var element in result)
            {
                var reference = element.FindElement(By.TagName("a")).Text;
                var text = element.Text;

                // Removing the verseReference from scriptureText
                var stringToRemove = reference + " - ";
                int index = text.IndexOf(stringToRemove);
                if (index > -1)
                {
                    text = text.Remove(index, stringToRemove.Length);
                }
                try
                {
                    bibleVerses = bibleVerses.Append(new BibleVerse(reference, text));
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"{reference}");
                }
            }
            return bibleVerses;
        }
    }
}