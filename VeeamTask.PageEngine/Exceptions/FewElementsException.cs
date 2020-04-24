using System;
using VeeamTask.PageEngine.WebElements;

namespace VeeamTask.PageEngine.Exceptions
{
    public class FewElementsException : Exception
    {
        public FewElementsException(UIElement element, int Count) :
            base($"Find {Count} elements instead of one for Element '{element.WebElement.GetType()}'")
        {
        }
    }
}