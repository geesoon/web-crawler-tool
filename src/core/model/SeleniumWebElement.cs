using BLBConcordance.Core.Interfaces;
using OpenQA.Selenium;

namespace BLBConcordance.Core.Model
{
    public class SeleniumWebElement : IElement
    {
        private readonly IWebElement webElement;
        public SeleniumWebElement(IWebElement webElement)
        {
            this.webElement = webElement;
        }
    }
}