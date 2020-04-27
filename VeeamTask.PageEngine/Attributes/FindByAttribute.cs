using System;
using OpenQA.Selenium;

namespace VeeamTask.PageEngine.Attributes
{
    
    public abstract class FindByAttribute : Attribute
    {
        protected By Locator;

        public By GetLocator()
        {
            return Locator;
        }
    }
}