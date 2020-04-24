using System;
using System.Diagnostics;
using System.Management;

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

        public static bool CheckProcessStart(string processName) => Process.GetProcessesByName(processName).Length > 0;

        internal static string GetProcessCommandLine(int pid)
        {
            using (var searcher = new ManagementObjectSearcher("Select * From Win32_Process Where ProcessID=" + pid))
            {
                using var moc = searcher.Get();
                foreach (var mo in moc)
                    return mo["CommandLine"].ToString();
            }
            throw new ArgumentException("pid");
        }

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