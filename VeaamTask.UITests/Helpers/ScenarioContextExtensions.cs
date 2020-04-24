using System.Diagnostics.CodeAnalysis;
using TechTalk.SpecFlow;

namespace VeeamTask.UITests.Helpers
{
    public static class ScenarioContextExtensions
    {
        public static void SaveCountOfFoundJobs(this ScenarioContext scenarioContext, int foundJobs) =>
            scenarioContext.AddOrUpdate("FoundJobs", foundJobs);

        public static int GetSavedCountOfFoundJobs(this ScenarioContext scenarioContext) =>
            scenarioContext.Get<int>("FoundJobs");

        /// <summary>
        /// Extension for TechTalk.SpecFlow.ScenarioContext. Method that checks and removes the key before adding
        /// </summary>
        /// <param name="scenarioContext">Any ScenarioContext</param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void AddOrUpdate(this ScenarioContext scenarioContext, [NotNull]string key, object value)
        {
            if (scenarioContext.ContainsKey(key))
                scenarioContext.Remove(key);

            scenarioContext.Add(key, value);
        }

    }
}