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
        private static string _output;

        private readonly DataReceivedEventHandler _eventHandler = new DataReceivedEventHandler((sender, e) => _output += e.Data);
        private readonly string _appPath = Path.Combine(Path.GetDirectoryName(Assembly.GetAssembly(typeof(ProcessHelper)).Location),
        "VeeamTask.MonitorApp.exe").Replace(".Tests", "");

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
            ProcessHelper.StartProcess("cmd.exe");
            System.Threading.Thread.Sleep(2000);
            ProcessHelper.StartProcess(_appPath, _eventHandler, "cmd", "1", "1");

            System.Threading.Thread.Sleep(120000);
            Assert.Contains("The process is already running", _output);
        }

        [Fact]
        public void CheckProcessKilling()
        {
            ProcessHelper.StartProcess(_appPath, _eventHandler, "cmd", "1", "1");
            ProcessHelper.StartProcess("cmd.exe");
            System.Threading.Thread.Sleep(2000);

            System.Threading.Thread.Sleep(120000);
            Assert.Contains("has been killed", _output);
            Assert.Contains("Process stopped", _output);
        }

        [Fact]
        public void CheckReceiveProcessAfterStart()
        {
            ProcessHelper.StartProcess(_appPath, _eventHandler, "cmd", "1", "1");
            ProcessHelper.StartProcess("cmd.exe");
            System.Threading.Thread.Sleep(2000);

            System.Threading.Thread.Sleep(120000);
            Assert.Contains("Process started", _output);
        }

        public void Dispose()
        {
            _output = null;
            ProcessHelper.KillAllProcess("cmd");
            ProcessHelper.KillAllProcess("VeeamTask.MonitorApp");
        }
    }
}