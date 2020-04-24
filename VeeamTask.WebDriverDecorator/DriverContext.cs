using System;
using System.IO;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using VeeamTask.MonitorApp.Engine.Helpers;
using VeeamTask.WebDriverDecorator.Interfaces;

namespace VeeamTask.WebDriverDecorator
{
    public class DriverContext : IDriverContext, IDriverProcessHandler, IDisposable
    {
        private WebDriverWait _wait;
        public WebDriverWait Wait
        {
            get => _wait ??= new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            set => _wait = value;
        }

        private IWebDriver _driver;
        public IWebDriver Driver
        {
            get
            {
                if (_driver != null)
                    return _driver;
                try
                {
                    _driver = new ChromeDriver();
                }
                catch
                {
                    _driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                }

                return _driver;
            }
            set => _driver = value;
        }

        /// <summary>
        /// Method for getting web page cookies 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Cookie GetCookieNamed(string name) => Driver.Manage().Cookies.GetCookieNamed(name);

        /// <summary>
        /// Method for killing all running driver processes
        /// </summary>
        public void KillAllRunWebDrivers() =>
            ProcessHelper.KillAllProcess("chromedriver");

        public void Dispose()
        {
            _driver = null;
            _wait = null;
            KillAllRunWebDrivers();
        }
    }
}