using System;
using OpenQA.Selenium;
using VeeamTask.PageEngine.Interfaces;

namespace VeeamTask.PageEngine.Attributes.FindByAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ByIdAttribute : FindByAttribute, IFindByAttribute
    {
        public ByIdAttribute(string id)
        {
            Locator = By.Id(id);
        }
    }
}