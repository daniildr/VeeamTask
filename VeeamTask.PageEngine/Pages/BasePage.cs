using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using VeeamTask.PageEngine.Attributes;
using VeeamTask.PageEngine.Interfaces;
using VeeamTask.WebDriverDecorator;

namespace VeeamTask.PageEngine.Pages
{
    public class BasePage : IPage
    {
        public bool CheckAfterOpen { get; set; }

        public string Url { get; set; }

        public string Title { get; set; }

        public BasePage(WebPageAttribute pageAttribute, string domain)
        {
            Url = domain + pageAttribute.Url;

            Title = pageAttribute.Title;
            SetSeleniumPropertiesByAttributes(GetLocatorByAttribute());
        }

        public virtual void Open()
        {
            DriverContextHook.DriverContext.Driver.Navigate().GoToUrl(Url);
            if (!CheckAfterOpen)
                CheckOpened();
            DriverContextHook.DriverContext.Driver.Manage().Window.Maximize();
        }

        public void Close() => DriverContextHook.DriverContext.Driver.Quit();

        public bool CheckOpened() => CheckAfterOpen = DriverContextHook.DriverContext.Driver.Title == Title;

        public void Refresh()
        {
            DriverContextHook.DriverContext.Driver.Navigate().Refresh();
            CheckOpened();
        }

        /// <summary>
        /// IWebElement value assignment method
        /// </summary>
        /// <param name="locatorsTuple"></param>
        protected void SetSeleniumPropertiesByAttributes(List<(string, By)> locatorsTuple)
        {
            var properties = GetType().GetProperties();
            foreach (var property in properties.Where(i => i.PropertyType.GetInterfaces().Contains(typeof(IWebElement))))
            {
                var constructorInfo = property.PropertyType.GetConstructor(new[] { typeof(By) });
                if (constructorInfo != null)
                {
                    try
                    {
                        property.SetValue(this,
                            constructorInfo.Invoke(new object[] { locatorsTuple.Find(i => i.Item1 == property.Name).Item2 }));
                    }
                    catch (ArgumentException)
                    {
                        //ignore
                    }
                }

            }
        }

        /// <summary>
        /// Returns all OpenQA.Selenium.By elements that were specified in the page class attributes
        /// </summary>
        /// <returns></returns>
        protected List<(string, By)> GetLocatorByAttribute()
        {
            return (from prop in GetType().GetProperties()
                let attrs =
                    (FindByAttribute[])prop.GetCustomAttributes(typeof(FindByAttribute), false)
                from attr in attrs
                select (prop.Name, attr.GetLocator())).ToList();
        }
    }
}