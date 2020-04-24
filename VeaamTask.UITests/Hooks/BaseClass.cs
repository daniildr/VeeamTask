using System;
using BoDi;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;
using VeeamTask.WebDriverDecorator;
using VeeamTask.WebDriverDecorator.Interfaces;

namespace VeaamTask.UITests.Hooks
{
    public class BaseClass
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly IObjectContainer _objectContainer;
        private static ChromeOptions _chromeOptions;

        public BaseClass(IObjectContainer objectContainer, ScenarioContext scenarioContext)
        {
            _objectContainer = objectContainer;
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario(Order = 0)]
        [AfterScenario, Scope(Tag = "Category:UI")]
        public static void KillAllRunWebDrivers(IObjectContainer _objectContainer)
        {
            var driver = (DriverContext)_objectContainer.Resolve(typeof(DriverContext));
            driver.KillAllRunWebDrivers();
            DriverContextHook.CleanUp();
        }

        [BeforeScenario(Order = 99), Scope(Tag = "Category:UI")]
        public void SetDriver()
        {
            _chromeOptions = new ChromeOptions();
            _chromeOptions.AddArgument("--ignore-certificate-errors");

            var driverContext = new DriverContext
            {
                Driver = new ChromeDriver(_chromeOptions)
            };
            driverContext.Wait = new WebDriverWait(driverContext.Driver, TimeSpan.FromMilliseconds(12000));

            _objectContainer.RegisterInstanceAs(driverContext, typeof(IDriverContext));
            _objectContainer.RegisterInstanceAs(driverContext, typeof(DriverContext));
            _objectContainer.RegisterInstanceAs(driverContext.Driver);

            DriverContextHook.ConfigureHook(_objectContainer);
        }
    }
}