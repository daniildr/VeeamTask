using System.Collections.ObjectModel;
using OpenQA.Selenium;
using VeeamTask.PageEngine.WebElements;

namespace VeeamTask.PageObjects.WebElements
{
    public class FoundJobsTable : UIElement
    {
        private readonly By _jobItemLocator = By.XPath(".//div[contains(@class, 'vacancies-blocks-col')]");

        public FoundJobsTable(By byLocator) : base(byLocator) { }

        public ReadOnlyCollection<IWebElement> JobsItemCollection => WebElement.FindElements(_jobItemLocator);
    }
}