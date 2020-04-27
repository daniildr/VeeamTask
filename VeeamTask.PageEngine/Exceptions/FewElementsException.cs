using System;
using VeeamTask.PageEngine.WebElements;

namespace VeeamTask.PageEngine.Exceptions
{
    public class FewElementsException : Exception
    {
        public FewElementsException(UIElement element, int count) :
            base($"Find more the one elements instead of one for Element '{element.WebElement.GetType()}'")
        {
        }
    }
}