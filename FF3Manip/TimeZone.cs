using System;
using System.Diagnostics;
using System.Security.Permissions;
using System.Threading;

namespace FF3Manip
{
    public class TimeZone
    {
        public string SavedTimeZone = String.Empty;

        private static TimeZone _TimeZone = null;
        public static TimeZone Instance
        {
            get
            {
                if(_TimeZone == null)
                {
                    _TimeZone = new TimeZone();
                }
                return _TimeZone;
            }
        }

        public void SaveTimeZone()
        {
            SavedTimeZone = TimeZoneInfo.Local.StandardName;
        }
        
        public static void GetTimeZone()
        {
            string args = "/g";
            Process p = Process.Start("tzutil.exe", args);
            if (p != null)
            {
                p.WaitForExit();
                TimeZoneInfo.ClearCachedData();
            }
        }
        
        public void RevertTime()
        {
            Thread.Sleep(2000);
            // Revert Time zone
            string args = "/s \"" + SavedTimeZone + "\"";
            Process p = Process.Start("tzutil.exe", args);
            p.WaitForExit();
            if (p != null)
            {
                p.WaitForExit();
                TimeZoneInfo.ClearCachedData();
            }
            Console.WriteLine("Reverted timezone to " + SavedTimeZone);

            // Sync time
            ProcessStartInfo timeSync = new ProcessStartInfo("w32tm.exe");
            timeSync.Verb = "runas";
            timeSync.Arguments = "/resync";
            p = Process.Start(timeSync);
            if (p != null)
            {
                p.WaitForExit();
            }
        }

        public void SetTimeZone(string timezone)
        {
            string args = "/s \"" + timezone + "\"";
            Process p = Process.Start("tzutil.exe", args);
            if (p != null)
            {
                p.WaitForExit();
            }
        }
    }
}

public static class TimeZones
{
    public const string ET = "Eastern Standard Time";
    public const string UTC = "UTC";
    public const string JST = "Tokyo Standard Time";
    public const string GMT = "GMT Standard Time";
}