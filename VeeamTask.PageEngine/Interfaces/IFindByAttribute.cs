using OpenQA.Selenium;

namespace VeeamTask.PageEngine.Interfaces
{
    public interface IFindByAttribute
    {
        By GetLocator();
    }
}