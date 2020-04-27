using System;
using System.Linq;
using BoDi;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using VeeamTask.PageObjects.Sites;
using VeeamTask.UITests.Helpers;

namespace VeeamTask.UITests.Steps
{
    [Binding]
    public class JobsCounterSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly CareerVeeam _careerVeeam;

        public JobsCounterSteps(IObjectContainer objectContainer, ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _careerVeeam = objectContainer.Resolve<CareerVeeam>();
        }

        [Given(@"I have opened the career page")]
        public void GivenIHaveOpenedTheCareerPage()
        {
           _careerVeeam.JobSearchPage.Open();

           if (_careerVeeam.JobSearchPage.CheckAfterOpen == false)
               Assert.Fail($"Page {_careerVeeam.JobSearchPage.Title} ({_careerVeeam.JobSearchPage.Url}) is not opened");
        }
        
        [When(@"I remember the number of vacancies")]
        public void WhenIRememberTheNumberOfVacancies() =>
            _scenarioContext.SaveCountOfFoundJobs(_careerVeeam.JobSearchPage.JobsCounter);

        [When(@"I set country - (.*)")]
        public void WhenISetCountry_(string country) =>
            _careerVeeam.JobSearchPage.CountrySelectElement.SelectItemByValue(country);

        [When(@"I set languages")]
        public void WhenISetLanguages(Table languageTable)
        {
            var languageArr = languageTable.Rows[0].Values.ToArray();
            _careerVeeam.JobSearchPage.LanguagesSelectElement.SelectItemByValue(languageArr);
        }

        [When(@"I unset languages")]
        public void WhenIUnsetLanguages(Table languageTable)
        {
            var languageArr = languageTable.Rows[0].Values.ToArray();
            _careerVeeam.JobSearchPage.LanguagesSelectElement.UnselectItemByValue(languageArr);
        }

        [When(@"I click the show more button")]
        public void WhenIClickTheShowMoreButton()
        {
            try
            {
                _careerVeeam.JobSearchPage.ShowMoreButton.Click();
            }
            catch (ElementNotInteractableException)
            {
                // Ignore. Because sometimes this button may not be active
            }
        }

        [Then(@"Expected number of vacancies is equal to the initial")]
        public void ThenExpectedNumberOfVacanciesIsEqualToTheInitial() =>
            Assert.AreEqual(_scenarioContext.GetSavedCountOfFoundJobs(), _careerVeeam.JobSearchPage.JobsCounter,
                $"Expected number of vacancies is not equal to the initial. " +
                $"Expected number of vacancies is a jobs found number on the page. " +
                $"Initial quantity was obtained from the scenario context.");
        
        [Then(@"Expected jobs count equal - (.*)")]
        public void ThenExpectedJobsCountEqual_(int jobsCount) =>
            Assert.AreEqual(jobsCount, _careerVeeam.JobSearchPage.JobsCounter,
                $"Expected number of vacancies is does not match passed in step argument. " +
                $"Expected number of vacancies is a jobs found number on the page.");

        [Then(@"Actual jobs count equal - (.*)")]
        public void ThenActualJobsCountEqual_(int jobsCount) =>
            Assert.AreEqual(jobsCount, _careerVeeam.JobSearchPage.FoundJobsTable.JobsItemCollection.Count,
                $"Actual number of vacancies is does not match passed in step argument. " +
                $"Actual number of vacancies is a count of job preview items on the page.");
    }
}
