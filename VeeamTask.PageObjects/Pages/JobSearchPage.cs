using System.Collections.Generic;
using VeeamTask.PageEngine.Attributes;
using VeeamTask.PageEngine.Pages;
using VeeamTask.PageEngine.WebElements;
using VeeamTask.PageObjects.WebElements;

namespace VeeamTask.PageObjects.Pages
{
    public class JobSearchPage : BasePage
    {
        public JobSearchPage(WebPageAttribute pageAttribute, string domain) : base(pageAttribute, domain) {  }

        [FindBy(XPath = "//h3[contains(text(), 'jobs found')]")]
        public TextElement JobsCounterElement { protected get; set; }

        [FindBy(XPath = "//div[@class='container']/div[contains(@class, 'vacancies-blocks')]")]
        public FoundJobsTable FoundJobsTable { get; set; }

        public int JobsCounter => int.Parse(JobsCounterElement.Text.Replace("jobs found", ""));

        [FindBy(XPath = "//a[contains(@class, 'load-more-button')]")]
        public Button ShowMoreButton { get; set; }
    }
}