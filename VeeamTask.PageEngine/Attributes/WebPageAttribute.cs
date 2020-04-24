using System;

namespace VeeamTask.PageEngine.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class WebPageAttribute : Attribute
    {
        public string Url { get; set; }

        public string Title { get; set; }
    }
}