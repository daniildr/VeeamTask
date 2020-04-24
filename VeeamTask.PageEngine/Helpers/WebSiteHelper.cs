using System;
using System.Reflection;
using OpenQA.Selenium;
using VeeamTask.PageEngine.Attributes;
using VeeamTask.PageEngine.Pages;
using VeeamTask.PageEngine.Sites;

namespace VeeamTask.PageEngine.Helpers
{
    public static class WebSiteHelper
    {
        /// <summary>
        /// Returns the element "locator By" specified through the attributes
        /// </summary>
        /// <param name="page"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static By GetWebElementLocatorByAttribute(this BasePage page, string propertyName)
        {
            var webElementAttributeInfo =
                (page.GetType().GetProperty(propertyName) ?? throw new InvalidOperationException()).GetCustomAttribute<FindByAttribute>();

            return webElementAttributeInfo.GetLocator();
        }

        /// <summary>
        /// Returns domain string from webSite class attribute
        /// </summary>
        /// <param name="webSite"></param>
        /// <returns></returns>
        public static string GetDomainByWebSiteAttribute(BaseWebSite webSite) =>
            webSite.GetType().GetCustomAttribute<WebSiteAttribute>().Domain;

        /// <summary>
        /// Returns information about web page from WebPage Attribute
        /// </summary>
        /// <param name="webSite"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static WebPageAttribute GetWebPageAttributes(BaseWebSite webSite, string fieldName) =>
            webSite.GetType().GetField(fieldName).GetCustomAttribute<WebPageAttribute>();

        /// <summary>
        /// Returns information about website from WebSite Attribute
        /// </summary>
        /// <param name="webSite"></param>
        /// <returns></returns>
        public static WebSiteAttribute GetWebSiteAttribute(BaseWebSite webSite) =>
            webSite.GetType().GetCustomAttribute<WebSiteAttribute>();
    }
}