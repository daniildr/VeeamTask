using VeeamTask.PageEngine.Attributes;
using VeeamTask.PageEngine.Pages;
using VeeamTask.PageEngine.WebElements;
using VeeamTask.PageObjects.WebElements;

namespace VeeamTask.PageObjects.Pages
{
    public class JobSearchPage : BasePage
    {
        public JobSearchPage(WebPageAttribute pageAttribute, string domain) : base(pageAttribute, domain) {  }

        [FindBy(XPath = "//h3[contains(text(), 'found')]")]
        public TextElement JobsCounterElement { get; set; }

        public int JobsCounter =>
            int.Parse(
                JobsCounterElement.Text.Substring(0, JobsCounterElement.Text.IndexOf('j')));

        [FindBy(XPath = "//*[@id='country-element']//div[contains(@class, 'selecter ')]")]
        public CustomSelectElement CountrySelectElement { get; set; }

        [FindBy(Id = "language")]
        public SelectWithCheckBoxElement LanguagesSelectElement { get; set; }

        [FindBy(XPath = "//div[@class='container']/div[contains(@class, 'vacancies-blocks')]")]
        public FoundJobsTable FoundJobsTable { get; set; }

        [FindBy(XPath = "//a[contains(@class, 'load-more-button')]")]
        public Button ShowMoreButton { get; set; }
    }
}