using System;
using VeeamTask.MonitorApp.Engine.Helpers;

namespace VeeamTask.MonitorApp
{
    internal class Program
    {
        private static string _processName;
        private static int _schedulerTime;
        private static int _maxMinutesLife;

        public static void Main(string[] args)
        {
            switch (args.Length)
            {
                case 0:
                    throw new ArgumentNullException(nameof(_processName), $"Process name not specified" );
                case 1:
                    _processName = args[0];
                    ProcessHelper.KillAllProcess(_processName);
                    break;
                case 2:
                    _processName = args[0];
                    _schedulerTime = int.Parse(args[1]);
                    break;
                case 3:
                    _processName = args[0];
                    _schedulerTime = int.Parse(args[1]);
                    _schedulerTime = int.Parse(args[2]);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(
                        $"Number of arguments exceeds possible. Arguments: {string.Join(", ", args)}");
            }


            Console.ReadLine();
        }
    }
}
