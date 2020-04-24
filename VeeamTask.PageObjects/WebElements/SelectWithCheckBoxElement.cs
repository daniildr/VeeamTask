using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using OpenQA.Selenium;
using VeeamTask.PageEngine.WebElements;

namespace VeeamTask.PageObjects.WebElements
{
    public class SelectWithCheckBoxElement : CustomSelectElement
    {
        public ReadOnlyCollection<IWebElement> AvailableItemsCollection =>
            DropdownElement.FindElements(GetItemLocator());

        private static Button ApplyButton => new Button(By.XPath("//a[contains(@class, 'submit') and contains(text(), 'Apply')]"));

        public SelectWithCheckBoxElement(By byLocator) : base(byLocator) { }

        public void SelectItemByName(params string[] itemNames) =>
            ClickOnItems(itemNames);

        public void UnselectItemByName(params string[] itemNames) =>
            ClickOnItems(itemNames);

        private void ClickOnItems(params string[] itemNames)
        {
            WebElement.Click();

            foreach (var name in itemNames)
            {
                AvailableItemsCollection.First(x => x.Text == name).Click();
            }

            ApplyButton.Click();
        }

        private static By GetItemLocator() =>
            By.XPath($".//fieldset//label");
    }
}