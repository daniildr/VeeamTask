using System;
using System.Diagnostics.CodeAnalysis;
using System.Management;
using System.Threading.Tasks;
using VeeamTask.MonitorApp.Engine.Helpers;

namespace VeeamTask.MonitorApp
{
    internal class MonitorApp : IDisposable
    {
        [NotNull] private static string _processName;
        [NotNull] private static TimeSpan _schedulerTime;
        [NotNull] private static TimeSpan _maxMinutesLife;

        private static DateTime? _processStarTime;

        public static void Main(params string[] args)
        {
            if (args.Length < 3)
                throw new ArgumentOutOfRangeException(
                    $"Number of arguments is less than necessary. Arguments: {string.Join(", ", args)}");
            if(args.Length > 3)
                throw new ArgumentOutOfRangeException(
                    $"Number of arguments exceeds possible. Arguments: {string.Join(", ", args)}");

            _processName = args[0].Trim();
            _schedulerTime = TimeSpan.FromMinutes(int.Parse(args[1]));
            _maxMinutesLife = TimeSpan.FromMinutes(int.Parse(args[2]));

            Console.WriteLine("Press any key to exit. \r\n");

            if (ProcessHelper.CheckProcessStart(_processName))
            {
                Console.WriteLine("The process is already running. Time control started.\r\n");
                _processStarTime = DateTime.Now;
            }

            var startWatch = ProcessHelper.GetProcessStartWatcher();
            startWatch.EventArrived += StartWatchEventArrived;
            startWatch.Start();

            var stopWatch = ProcessHelper.GetProcessStopWatcher();
            stopWatch.EventArrived += StopWatchEventArrived;
            stopWatch.Start();

            var monitoring = ProcessMonitoringTask();
            monitoring.Start();
            
            while (!Console.KeyAvailable)
            {
                System.Threading.Thread.Sleep(50);
            }
            startWatch.Stop();
            stopWatch.Stop();

            Console.ReadLine();
        }

        private static void StartWatchEventArrived(object sender, EventArrivedEventArgs e)
        {
            if (!e.NewEvent.Properties["ProcessName"].Value.ToString().Contains(_processName)) return;

            _processStarTime = DateTime.Now;
            Console.WriteLine($"Process started: {e.NewEvent.Properties["ProcessName"].Value}. " +
                              $"Time tracking started.\r\n");
        }

        private static void StopWatchEventArrived(object sender, EventArrivedEventArgs e)
        {
            if (!e.NewEvent.Properties["ProcessName"].Value.ToString().Contains(_processName)) return;

            _processStarTime = null;
            Console.WriteLine($"Process stopped: {e.NewEvent.Properties["ProcessName"].Value}. " +
                              $"Time tracking stopped.\r\n");
        }

        private static Task ProcessMonitoringTask()
        {
            return new Task(() =>
            {
                while (true)
                {
                    if (_processStarTime == null) continue;

                    if (DateTime.Now - _processStarTime < _schedulerTime) continue;

                    if (DateTime.Now - _processStarTime > _maxMinutesLife)
                        KillProcessAndSendResults(_processName);
                }
            });
        }

        private static void KillProcessAndSendResults(string processName)
        {
            if(!ProcessHelper.CheckProcessStart(processName))
                Console.WriteLine($"Process {processName} is not running.\r\n");
            else
            {
                ProcessHelper.KillAllProcess(_processName);
                _processStarTime = null;
                Console.Write($"Process {_processName} has been killed.\r\n");
            }
        }

        public void Dispose()
        {
            _processName = null;
            _processStarTime = null;
        }
    }
}
