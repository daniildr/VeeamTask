using OpenQA.Selenium;

namespace VeeamTask.PageEngine.Interfaces
{
    public interface IWebSite
    {
        void SetCookies(Cookie cookie);

        void DeleteCookies();
    }
}