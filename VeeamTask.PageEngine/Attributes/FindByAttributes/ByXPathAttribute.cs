using System;
using OpenQA.Selenium;
using VeeamTask.PageEngine.Interfaces;

namespace VeeamTask.PageEngine.Attributes.FindByAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ByXPathAttribute : FindByAttribute, IFindByAttribute
    {
        public ByXPathAttribute(string xPathExpression)
        {
            Locator = By.XPath(xPathExpression);
        }
    }
}