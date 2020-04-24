using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace VeeamTask.WebDriverDecorator.Interfaces
{
    public interface IDriverContext
    {
        WebDriverWait Wait { get; set; }

        /// <summary>
        /// Web driver instance
        /// </summary>
        IWebDriver Driver { get; set; }

        /// <summary>
        /// Method for getting web page cookies 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Cookie GetCookieNamed(string name);
    }
}