using System;
using BoDi;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;
using VeeamTask.WebDriverDecorator;
using VeeamTask.WebDriverDecorator.Interfaces;

namespace VeeamTask.UITests.Hooks
{
    [Binding]
    public class BaseClass
    {
        protected ScenarioContext ScenarioContext;
        protected IObjectContainer ObjectContainer;
        private static ChromeOptions _chromeOptions;

        public BaseClass(IObjectContainer objectContainer, ScenarioContext scenarioContext)
        {
            ObjectContainer = objectContainer;
            ScenarioContext = scenarioContext;
        }
        
        [SetUp]
        [BeforeScenario(Order = 0)]
        [Scope(Tag = "UI")]
        public void SetDriver()
        {
            _chromeOptions = new ChromeOptions();
            _chromeOptions.AddArgument("--ignore-certificate-errors");

            var driverContext = new DriverContext
            {
                Driver = new ChromeDriver(_chromeOptions)
            };
            driverContext.Wait = new WebDriverWait(driverContext.Driver, TimeSpan.FromMilliseconds(12000));

            ObjectContainer.RegisterInstanceAs(driverContext, typeof(IDriverContext), dispose: true);
            ObjectContainer.RegisterInstanceAs(driverContext, typeof(DriverContext), dispose: true);

            DriverContextHook.ConfigureHook(ObjectContainer);
        }

        [AfterScenario(Order = 0)]
        [Scope(Tag = "UI")]
        public static void KillAllRunWebDrivers(IObjectContainer _objectContainer)
        {
            var driver = (DriverContext)_objectContainer.Resolve(typeof(DriverContext));
            driver.KillAllRunWebDrivers();
            DriverContextHook.CleanUp();
        }

        [BeforeStep(Order = 99)] 
        [AfterStep(Order = 99)]
        [Scope(Tag = "UI")]
        public static void SeleniumPause()
        {
            System.Threading.Thread.Sleep(800);
        }
    }
}