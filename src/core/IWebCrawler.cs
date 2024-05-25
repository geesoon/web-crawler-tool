using OpenQA.Selenium;

namespace BLBConcordance.Core
{
    /// <summary>
    /// An abstraction of web crawler
    /// </summary>
    public interface IWebCrawler
    {
        /// <summary>
        /// Find element from the web page 
        /// </summary>
        /// <param name="by"></param>
        /// <returns>A list of read only IWebElement</returns> 
        /// <summary>
        public IReadOnlyList<IWebElement> FindElements(By by);
        public void BrowseUrl(string Url);
    }
}