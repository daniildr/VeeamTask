using System;

namespace VeeamTask.PageEngine.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class WebSiteAttribute : Attribute
    {
        public string Domain { get; set; }
    }
}