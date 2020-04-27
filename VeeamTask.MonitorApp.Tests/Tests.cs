using System;
using System.Diagnostics;
using System.Threading.Tasks;
using VeeamTask.MonitorApp.Engine.Helpers;
using Xunit;

namespace VeeamTask.MonitorApp.Tests
{
    public class Tests
    {
        private MonitorApp MonitorApp;
        private string Output;

        public Tests()
        {
            MonitorApp = new MonitorApp();
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

        //[Theory]
        //[InlineData("cmd", "1", "1")]
        //public void CheckProcessAlreadyStarted(params string[] args)
        //{
        //    var eventHandler = new DataReceivedEventHandler((sender, e) => Output += e.Data);

        //    ProcessHelper.StartProcess("cmd.exe");
        //    System.Threading.Thread.Sleep(2000);
        //    ProcessHelper.StartProcess();


        //    var monitor = new Task(() => MonitorApp.Main(args));
        //    monitor.Start();


        //    //Assert.True(Console.);
        //    var qwe = 0;

        //    System.Threading.Thread.Sleep(180000);
        //    qwe = 1;
        //}


        
    }
}