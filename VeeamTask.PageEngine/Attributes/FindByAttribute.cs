using System;
using OpenQA.Selenium;

namespace VeeamTask.PageEngine.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FindByAttribute : Attribute
    {
        private By _locator;
        public string Id
        {
            set => _locator = By.Id(value);
        }
        public string Name
        {
            set => _locator = By.Name(value);
        }
        public string ClassName
        {
            set => _locator = By.ClassName(value);
        }
        public string Css
        {
            set => _locator = By.CssSelector(value);
        }
        public string XPath
        {
            set => _locator = By.XPath(value);
        }
        public string Tag
        {
            set => _locator = By.TagName(value);
        }
        public string LinkText
        {
            set => _locator = By.LinkText(value);
        }
        public string PartialLinkText
        {
            set => _locator = By.PartialLinkText(value);
        }

        public By GetLocator()
        {
            return _locator;
        }
    }
}