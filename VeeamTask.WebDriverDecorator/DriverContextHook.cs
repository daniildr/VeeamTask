using BoDi;
using VeeamTask.WebDriverDecorator.Interfaces;

namespace VeeamTask.WebDriverDecorator
{
    public class DriverContextHook
    {
        private readonly IObjectContainer _objectContainer;

        public DriverContextHook(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        private static IDriverContext _driverContext;
        public static IDriverContext DriverContext
        {
            get => _driverContext ??= new DriverContext();
            set => _driverContext = value;
        }

        /// <summary>
        /// Method for configure hook. Use this after registration the Driver Context in BoDi IoC 
        /// </summary>
        /// <param name="objectContainer"></param>
        public static void ConfigureHook(IObjectContainer objectContainer)
        {
            var hook = new DriverContextHook(objectContainer);
            if (hook.CheckDriverInIoC())
                hook.SetDriverInstanceByIoC();
            else
                _driverContext = new DriverContext();
        }

        /// <summary>
        /// Method for cleanup instance of Driver Context static hook 
        /// </summary>
        public static void CleanUp() => _driverContext = null;

        private bool CheckDriverInIoC() =>
            _objectContainer.IsRegistered<IDriverContext>();

        private void SetDriverInstanceByIoC() =>
            _driverContext = _objectContainer.Resolve<IDriverContext>();
    }
}