using OpenQA.Selenium;
using VeeamTask.PageEngine.WebElements;

namespace VeeamTask.PageObjects.WebElements
{
    public class CustomSelectElement : UIElement
    {
        protected IWebElement DropdownElement =>
            WebElement.FindElement(By.XPath(".//div[contains(@class, 'selecter-options')]"));

        public CustomSelectElement(By byLocator) : base(byLocator) { }

        /// <summary>
        /// Method for set value of custom selector
        /// </summary>
        /// <param name="itemValue">Item name or value</param>
        public void SelectItemByValue(string itemValue)
        {
            WebElement.Click();

            DropdownElement.FindElement(GetItemLocator(itemValue)).Click();
        }

        protected By GetItemLocator(string text) =>
            By.XPath($".//div[contains(@class, 'scroller-content')]//span[contains(text(), '{text}')]");
    }
}