using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using OpenQA.Selenium;
using VeeamTask.WebDriverDecorator.Exceptions;

namespace VeeamTask.WebDriverDecorator.Extensions
{
    public static class SeleniumExtensions
    {
        /// <summary>
        /// Method for checking attribute availability on a web element
        /// </summary>
        /// <param name="element"></param>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        public static bool IsAttributeAvailable(this IWebElement element, string attributeName)
        {
            try
            {
                element.GetAttribute(attributeName);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}