using OpenQA.Selenium;
using VeeamTask.PageEngine.Interfaces;
using VeeamTask.WebDriverDecorator;

namespace VeeamTask.PageEngine.Sites
{
    public class BaseWebSite : IWebSite
    {
        public string Domain { get; set; }

        public void SetCookies(Cookie cookie)
        {
            DriverContextHook.DriverContext.Driver.Manage().Cookies.AddCookie(cookie);
        }

        public void DeleteCookies()
        {
            DriverContextHook.DriverContext.Driver.Manage().Cookies.DeleteAllCookies();
        }
    }
}