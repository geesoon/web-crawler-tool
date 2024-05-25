using OpenQA.Selenium;

namespace BLBConcordance.Core
{
    public class SeleniumWebElement : IElement
    {
        private readonly IWebElement webElement;
        public SeleniumWebElement(IWebElement webElement) {
            this.webElement = webElement;
        }
    }
}