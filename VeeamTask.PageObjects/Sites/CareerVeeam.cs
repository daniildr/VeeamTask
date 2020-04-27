using VeeamTask.PageEngine.Attributes;
using VeeamTask.PageEngine.Helpers;
using VeeamTask.PageEngine.Sites;
using VeeamTask.PageObjects.Pages;

namespace VeeamTask.PageObjects.Sites
{
    [WebSite(Domain = "https://careers.veeam.com")]
    public class CareerVeeam : BaseWebSite
    {
        [WebPage(Url = "/", Title = "Career at Veeam Software")]
        public JobSearchPage JobSearchPage;

        public CareerVeeam()
        {
            Domain = WebSiteHelper.GetDomainByWebSiteAttribute(this);
            JobSearchPage = new JobSearchPage(WebSiteHelper.GetWebPageAttributes(this, nameof(JobSearchPage)), Domain);
        }
    }
}