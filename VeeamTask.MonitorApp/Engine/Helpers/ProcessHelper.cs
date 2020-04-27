using System;
using System.Management;
using System.Diagnostics;

namespace VeeamTask.MonitorApp.Engine.Helpers
{
    public class ProcessHelper
    {
        public static void KillAllProcess(string processName)
        {
            try
            {
                foreach (var proc in Process.GetProcessesByName(processName))
                    KillProcessTree(proc.Id);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public static bool CheckProcessStart(string processName) => 
            Process.GetProcessesByName(processName).Length > 0;

        internal static void StartProcess(string processName,
            DataReceivedEventHandler eventHandler = null, params string[] args)
        {
            var processInfo = new ProcessStartInfo(processName, string.Join(" ", args))
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true
            };
            var process = Process.Start(processInfo);

            if (eventHandler == null) return;

            if (process != null)
            {
                process.OutputDataReceived += eventHandler;
                process.BeginOutputReadLine();
            }
            else
                throw new Exception("Process not started.");
        }

        internal static ManagementEventWatcher GetProcessStartWatcher() => 
            new ManagementEventWatcher(new WqlEventQuery("SELECT * FROM Win32_ProcessStartTrace"));

        internal static ManagementEventWatcher GetProcessStopWatcher() =>
            new ManagementEventWatcher(new WqlEventQuery("SELECT * FROM Win32_ProcessStopTrace"));

        internal static void KillProcessTree(int pid)
        {
            using var searcher = new ManagementObjectSearcher("Select * From Win32_Process Where ParentProcessID=" + pid);
            using var moc = searcher.Get();
            foreach (var mo in moc)
                KillProcessTree(Convert.ToInt32(mo["ProcessID"]));

            try
            {
                var proc = Process.GetProcessById(pid);
                proc.Kill();
            }
            catch (ArgumentException)
            {
                Console.WriteLine($"Process {pid} already exited");
            }
        }
    }
}