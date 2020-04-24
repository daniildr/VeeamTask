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
            get => throw new NotImplementedException();
            set => _locator = By.Id(value);
        }
        public string Name
        {
            get => throw new NotImplementedException();
            set => _locator = By.Name(value);
        }
        public string ClassName
        {
            get => throw new NotImplementedException();
            set => _locator = By.ClassName(value);
        }
        public string Css
        {
            get => throw new NotImplementedException();
            set => _locator = By.CssSelector(value);
        }
        public string XPath
        {
            get => throw new NotImplementedException();
            set => _locator = By.XPath(value);
        }
        public string Tag
        {
            get => throw new NotImplementedException();
            set => _locator = By.TagName(value);
        }
        public string LinkText
        {
            get => throw new NotImplementedException();
            set => _locator = By.LinkText(value);
        }
        public string PartialLinkText
        {
            get => throw new NotImplementedException();
            set => _locator = By.PartialLinkText(value);
        }

        public By GetLocator()
        {
            return _locator;
        }
    }
}