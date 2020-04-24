using OpenQA.Selenium;

namespace VeeamTask.PageEngine.Interfaces
{
    public interface IBaseUIElement : IWebElement
    {

        By Locator { get; set; }

        IWebElement WebElement { get; set; }
    }
}