using System;
using VeeamTask.PageEngine.WebElements;

namespace VeeamTask.PageEngine.Exceptions
{
    public class FewElementsException : Exception
    {
        public FewElementsException(UIElement element, int count) :
            base($"Find {count} elements instead of one for Element '{element.WebElement.GetType()}'")
        {
        }
    }
}