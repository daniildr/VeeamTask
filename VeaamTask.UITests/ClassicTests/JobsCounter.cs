using BoDi;
using NUnit.Framework;
using TechTalk.SpecFlow;
using VeeamTask.PageObjects.Sites;
using VeeamTask.UITests.Hooks;
using VeeamTask.UITests.Steps;
using VeeamTask.WebDriverDecorator;


namespace VeeamTask.UITests.ClassicTests
{
    [TestFixture]
    public class JobsCounter : BaseClass
    {
        private CareerVeeam CareerVeeam { get; }
        private JobsCounterSteps TestSteps { get; }

        public JobsCounter() : base(new ObjectContainer(), ScenarioContext.Current)
        {
            TestSteps = new JobsCounterSteps(ObjectContainer, ScenarioContext);
            CareerVeeam = ObjectContainer.Resolve<CareerVeeam>();
        }

        [Test]
        [TestCase("Romania", "English", 29)]
        [TestCase("Russian Federation", "English", 10)]
        public void ReusingScenarioSteps(string country, string language, int jobCount) 
        {
            // Arrange
            TestSteps.GivenIHaveOpenedTheCareerPage();
            SeleniumPause();
            var langTable = new Table("language");
            langTable.AddRow(language);

            // Act
            TestSteps.WhenISetCountry_(country);
            TestSteps.WhenISetLanguages(langTable);
            SeleniumPause();
            TestSteps.WhenIClickTheShowMoreButton();
            SeleniumPause();

            // Assert
            TestSteps.ThenExpectedJobsCountEqual_(jobCount);
            TestSteps.ThenActualJobsCountEqual_(jobCount);
        }

        [Test]
        [TestCase("Romania", "English", 29)]
        [TestCase("Russian Federation", "English", 10)]
        public void CheckForChangesInCount(string country, string language, int jobCount)
        {
            // Arrange
            CareerVeeam.JobSearchPage.Open();

            // Act
            CareerVeeam.JobSearchPage.CountrySelectElement.SelectItemByValue(country);
            CareerVeeam.JobSearchPage.LanguagesSelectElement.SelectItemByValue(language);
            SeleniumPause();
            if (jobCount > 12)
                CareerVeeam.JobSearchPage.ShowMoreButton.Click();

            // Assert
            Assert.AreEqual(jobCount, CareerVeeam.JobSearchPage.JobsCounter,
                $"Expected number of vacancies is does not match passed in step argument. " +
                $"Expected number of vacancies is a jobs found number on the page.");

            Assert.AreEqual(jobCount, CareerVeeam.JobSearchPage.FoundJobsTable.JobsItemCollection.Count,
                $"Actual number of vacancies is does not match passed in step argument. " +
                $"Actual number of vacancies is a count of job preview items on the page.");
        }

        [TearDown]
        public void CleanUp()
        {
            var driver = (DriverContext)ObjectContainer.Resolve(typeof(DriverContext));
            driver.KillAllRunWebDrivers();
            DriverContextHook.CleanUp();

            ObjectContainer = new ObjectContainer();
        }
    }
}