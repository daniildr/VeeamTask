using VeeamTask.PageEngine.Attributes;
using VeeamTask.PageEngine.Attributes.FindByAttributes;
using VeeamTask.PageEngine.Pages;
using VeeamTask.PageEngine.WebElements;
using VeeamTask.PageObjects.WebElements;

namespace VeeamTask.PageObjects.Pages
{
    public class JobSearchPage : BasePage
    {
        public JobSearchPage(WebPageAttribute pageAttribute, string domain) : base(pageAttribute, domain) {  }

        [ByXPath("//h3[contains(text(), 'found')]")]
        public TextElement JobsCounterElement { get; set; }

        public int JobsCounter =>
            int.Parse(
                JobsCounterElement.GetText().Substring(0, JobsCounterElement.Text.IndexOf('j')));

        [ByXPath("//*[@id='country-element']//div[contains(@class, 'selecter ')]")]
        public CustomSelectElement CountrySelectElement { get; set; }

        [ById("language")]
        public SelectWithCheckBoxElement LanguagesSelectElement { get; set; }

        [ByXPath("//div[@class='container']/div[contains(@class, 'vacancies-blocks')]")]
        public FoundJobsTable FoundJobsTable { get; set; }

        [ByXPath("//a[contains(@class, 'load-more-button')]")]
        public Button ShowMoreButton { get; set; }
    }
}