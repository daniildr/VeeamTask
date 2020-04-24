using OpenQA.Selenium;

namespace VeeamTask.PageEngine.WebElements
{
    public class TextElement : UIElement
    {
        public new string Text => GetText();

        public TextElement(By byLocator) : base(byLocator)
        {
        }

        public string Value => GetTextAction();

        public virtual string GetValue() => Value;

        public virtual string GetText() => Value;

        protected virtual string GetTextAction()
        {
            if (string.IsNullOrEmpty(WebElement.Text))
                return WebElement.GetAttribute("value") ?? string.Empty;
            
            return WebElement.Text;
        }
    }
}