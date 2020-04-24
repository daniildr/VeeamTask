using BoDi;
using NUnit.Framework;
using TechTalk.SpecFlow;
using VeeamTask.UITests.Hooks;
using VeeamTask.UITests.Steps;
using VeeamTask.WebDriverDecorator;


namespace VeeamTask.UITests.ClassicTests
{
    [TestFixture]
    public class JobsCounter : BaseClass
    {
        private JobsCounterSteps TestSteps { get; set; }

        public JobsCounter() : base(new ObjectContainer(), ScenarioContext.Current)
        {
            TestSteps = new JobsCounterSteps(ObjectContainer, ScenarioContext);
        }

        [Test]
        [TestCase("Romania", "English", 30)]
        [TestCase("Russian Federation", "English", 10)]
        public void CheckForChangesInCount(string country, string language, int jobCount) 
        {
            //Arrange
            TestSteps.GivenIHaveOpenedTheCareerPage();
            SeleniumPause();
            var langTable = new Table("language");
            langTable.AddRow(language);

            //Act
            TestSteps.WhenISetCountry_(country);
            SeleniumPause();
            TestSteps.WhenISetLanguages(langTable);
            SeleniumPause();
            if (jobCount > 12)
                TestSteps.WhenIClickTheShowMoreButton();
            SeleniumPause();

            //Assert
            TestSteps.ThenExpectedJobsCountEqual_(jobCount);
            TestSteps.ThenActualJobsCountEqual_(jobCount);
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