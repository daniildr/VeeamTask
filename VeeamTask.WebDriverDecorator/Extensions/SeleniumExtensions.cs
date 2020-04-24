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
        /// Method for clean text field in React form or any text field where the standard method 'clean' does not work
        /// </summary>
        /// <param name="element"></param>
        /// <param name="needPressEndKey">Bool flag to go on last item in delete string (field value)</param>
        /// <param name="keepPressingEndKey"></param>
        public static void CleanWithBackspace(this IWebElement element, bool needPressEndKey = false, bool keepPressingEndKey = true)
        {
            element.Click();

            if (needPressEndKey)
                element.SendKeys(Keys.End);

            do
            {
                if (keepPressingEndKey)
                {
                    element.SendKeys(Keys.End);
                    element.SendKeys(Keys.Backspace);
                }
                else
                {
                    element.SendKeys(Keys.Backspace);
                }
            } while (element.GetAttribute("value").Length != 0);
        }

        /// <summary>
        /// The method checks the availability of the element. Checks for the presence of an HTML tag name
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static bool IsElementAvailable(this IWebElement element)
        {
            try
            {
                var unused = element.TagName;
                return true;
            }
            catch (ElementNotFoundException)
            {
                return false;
            }
        }

        /// <summary>
        /// The method checks the availability of the element. Searches child element by locator
        /// </summary>
        /// <param name="element"></param>
        /// <param name="locatorBy"></param>
        /// <returns></returns>
        public static bool IsElementAvailable(this IWebElement element, [NotNull]By locatorBy)
        {
            try
            {
                var unused = element.FindElement(locatorBy);
                return true;
            }
            catch (ElementNotFoundException)
            {
                return false;
            }
        }

        /// <summary>
        /// The method checks the availability of the element. Searches element by locator
        /// </summary>
        /// <param name="locatorBy"></param>
        /// <returns></returns>
        public static bool IsElementAvailable(By locatorBy)
        {
            try
            {
                var unused = DriverContextHook.DriverContext.Driver.FindElement(locatorBy);
                return true;
            }
            catch (ElementNotFoundException)
            {
                return false;
            }
        }

        /// <summary>
        /// Method for checking the presents to an element on the screen
        /// </summary>
        /// <param name="element"></param>
        /// <param name="by"></param>
        /// <returns></returns>
        public static bool IsElementPresent(this IWebElement element, By by)
        {
            const bool present = false;
            try
            {
                element.FindElement(by);
            }
            catch
            {
                return present;
            }

            return true;
        }

        /// <summary>
        /// Method for checking the ability to click on an element
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static bool IsClickable(this IWebElement element) => element.Enabled;

        /// <summary>
        /// Method to switch web driver to tab by predicate expression
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="predicateExp"></param>
        public static void SwitchToWindow(this IWebDriver driver, Expression<Func<IWebDriver, bool>> predicateExp)
        {
            var predicate = predicateExp.Compile();
            foreach (var handle in driver.WindowHandles)
            {
                driver.SwitchTo().Window(handle);
                if (predicate(driver))
                {
                    return;
                }
            }

            throw new ArgumentException($"Unable to find window with condition: '{predicateExp.Body}'");
        }

        /// <summary>
        /// Method to switch web driver to tab by title
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="title"></param>
        public static void SwitchToWindow(this IWebDriver driver, [NotNull]string title)
        {
            driver.SwitchToWindow(d => d.Title == title);
        }
    }
}