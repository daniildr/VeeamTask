using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using VeeamTask.MonitorApp.Engine.Helpers;
using Xunit;

namespace VeeamTask.MonitorApp.Tests
{
    public class Tests : IDisposable
    {
        private string Output;

        public Tests()
        {
            
        }

        [Theory]
        [InlineData("chromeDriver", "1", "2", "3")]
        [InlineData("chromeDriver", "1")]
        public void ArgumentsOutOfRangeExceptionsTest(params string[] args) =>
            Assert.Throws<ArgumentOutOfRangeException>(() => MonitorApp.Main(args));

        [Theory]
        [InlineData("chromeDriver", "qwe", "2")]
        [InlineData("chromeDriver", "1", "qwe")]
        public void ArgumentFormatExceptionsTest(params string[] args) =>
            Assert.Throws<FormatException>(() => MonitorApp.Main(args));

        [Fact]
        public void CheckProcessAlreadyStarted()
        {
            var eventHandler = new DataReceivedEventHandler((sender, e) => Output += e.Data);

            var appPath = Path.Combine(Path.GetDirectoryName(Assembly.GetAssembly(typeof(ProcessHelper)).Location),
                "VeeamTask.MonitorApp.exe").Replace(".Tests", "");

            ProcessHelper.StartProcess("cmd.exe");
            System.Threading.Thread.Sleep(2000);
            ProcessHelper.StartProcess(appPath, eventHandler, "cmd", "1", "1");

            System.Threading.Thread.Sleep(120000);
            Assert.Contains("The process is already running", Output);
        }


        public void Dispose()
        {
            Output = null;
            ProcessHelper.KillAllProcess("cmd");
            ProcessHelper.KillAllProcess("VeeamTask.MonitorApp");
        }
    }
}