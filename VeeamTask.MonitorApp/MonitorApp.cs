using System;
using System.Diagnostics.CodeAnalysis;
using System.Management;
using System.Threading.Tasks;
using VeeamTask.MonitorApp.Engine.Helpers;

namespace VeeamTask.MonitorApp
{
    internal class MonitorApp
    {
        private static readonly TimeSpan DelayTime = TimeSpan.FromSeconds(10);

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

            AppDomain.CurrentDomain.ProcessExit += ProcessExit;
            Console.WriteLine("Press any key to exit. \r\n");

            if (ProcessHelper.CheckProcessStart(_processName))
            {
                Console.WriteLine("The process is already running. Time control started.");
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
        }

        private static void StartWatchEventArrived(object sender, EventArrivedEventArgs e)
        {
            if (!e.NewEvent.Properties["ProcessName"].Value.ToString().Contains(_processName)) return;

            _processStarTime = DateTime.Now;
            Console.WriteLine($"Process started: {e.NewEvent.Properties["ProcessName"].Value}. " +
                              $"Time tracking started.");
        }

        private static void StopWatchEventArrived(object sender, EventArrivedEventArgs e)
        {
            if (!e.NewEvent.Properties["ProcessName"].Value.ToString().Contains(_processName)) return;

            CleanProcessStartTime();
            Console.WriteLine($"Process stopped: {e.NewEvent.Properties["ProcessName"].Value}. " +
                              $"Time tracking stopped.");
        }

        private static void ProcessExit(object sender, EventArgs e)
        {
            CleanProcessStartTime();
            Console.WriteLine("Application stopped. Thank you for using 'Daniil Dr .net lines'");
        }

        private static Task ProcessMonitoringTask()
        {
            return new Task(async () =>
            {
                while (true)
                {
                    // To continue, you must have information about the start time of the process
                    if (_processStarTime == null) { await Task.Delay(DelayTime); continue; }
                    
                    // If the verification time has not come, then continue monitoring
                    if (DateTime.Now - _processStarTime < _schedulerTime) { await Task.Delay(DelayTime); continue; }

                    // If the maximum lifetime is not reached, then continue monitoring
                    if (DateTime.Now - _processStarTime < _maxMinutesLife)
                    {
                        Console.WriteLine("Verification completed. The maximum lifetime has not been reached. The process continues to run.");
                        await Task.Delay(_schedulerTime); continue;
                    }

                    KillProcessAndSendResults(_processName);
                    CleanProcessStartTime();
                    await Task.Delay(DelayTime);
                }
            });
        }

        private static void KillProcessAndSendResults(string processName)
        {
            if(!ProcessHelper.CheckProcessStart(processName))
                Console.WriteLine($"Process {processName} is not running.");
            else
            {
                ProcessHelper.KillAllProcess(_processName);
                Console.WriteLine($"Process {_processName} has been killed.");
            }
        }

        private static void CleanProcessStartTime() =>
            _processStarTime = null;
    }
}
