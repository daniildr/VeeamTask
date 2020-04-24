using System.Collections.ObjectModel;
using System.Drawing;
using OpenQA.Selenium;
using VeeamTask.PageEngine.Exceptions;
using VeeamTask.WebDriverDecorator;
using VeeamTask.WebDriverDecorator.Exceptions;

namespace VeeamTask.PageEngine.WebElements
{
    public class UIElement : IWebElement
    {
        private IWebElement _webElement;
        public IWebElement WebElement
        {
            get
            {
                var result = DriverContextHook.DriverContext.Driver.FindElements(Locator);
                switch (result.Count)
                {
                    case 0:
                        throw new ElementNotFoundException($"Can't find Element '{this}'");
                    case 1:
                        break;
                    default:
                        if (OnlyOneElementAllowedInSearch)
                            throw new FewElementsException(this, result.Count);
                        break;
                }
                return _webElement = result[0];
            }
            set => _webElement = value;
        }

        public string TagName => WebElement.TagName;

        public string Text => WebElement.Text;

        public bool Enabled => WebElement.Enabled;

        public bool Selected => WebElement.Selected;

        public Point Location => WebElement.Location;

        public Size Size => WebElement.Size;

        public bool Displayed => !WebElement.Enabled;

        public By Locator { get; set; }

        public bool OnlyOneElementAllowedInSearch { get; set; } = true;

        public UIElement(By byLocator)
        {
            Locator = byLocator;
        }

        public IWebElement FindElement(By by) => WebElement.FindElement(by);

        public ReadOnlyCollection<IWebElement> FindElements(By @by) => WebElement.FindElements(by);

        public void Clear() => WebElement.Clear();

        public void SendKeys(string text) => WebElement.SendKeys(text);

        public void Submit() => WebElement.Submit();

        public void Click() => WebElement.Click();

        public string GetAttribute(string attributeName) => WebElement.GetAttribute(attributeName);

        public string GetProperty(string propertyName) => WebElement.GetProperty(propertyName);

        public string GetCssValue(string propertyName) => WebElement.GetCssValue(propertyName);
    }
}