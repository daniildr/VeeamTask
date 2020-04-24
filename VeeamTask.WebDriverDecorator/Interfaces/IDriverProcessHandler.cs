namespace VeeamTask.WebDriverDecorator.Interfaces
{
    public interface IDriverProcessHandler
    {
        /// <summary>
        /// Method for killing all running driver processes
        /// </summary>
        void KillAllRunWebDrivers();
    }
}