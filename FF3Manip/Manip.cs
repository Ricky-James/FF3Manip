using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;

namespace FF3Manip
{
    public class Manip
    {
        private static Manip _Manip = null;
        public static Manip Instance
        {
            get
            {
                if(_Manip == null)
                {
                    _Manip = new Manip();
                }
                return _Manip;
            }
        }

        private enum DateFormats
        {
            DDMMYYYY,
            MMDDYYYY,
            YYYYMMDD
        }
        
        public struct TargetDateTime
        {
            public short Day;
            public short Month;
            public short Year;
            public short Hour;
            public short Minute;
            public short Second;

            public TargetDateTime(short dd, short MM, short yyyy, short HH, short mm, short ss)
            {
                Day = dd;
                Month = MM;
                Year = yyyy;
                Hour = HH;
                Minute = mm;
                Second = ss;
            }
        }

        private DateFormats dateFormat;

        public Manip()
        {
            if (CultureInfo.CurrentCulture.DateTimeFormat.LongDatePattern.StartsWith("d"))
            {
                dateFormat = DateFormats.DDMMYYYY;
            }
            else if (CultureInfo.CurrentCulture.DateTimeFormat.LongDatePattern.StartsWith("m"))
            {
                dateFormat = DateFormats.MMDDYYYY;
            }
            else
            {
                dateFormat = DateFormats.YYYYMMDD;
            }
        }

        private bool GameRunning()
        {
            return Process.GetProcessesByName("FF3_Win32").Length > 0 ? true : false;
        }

        public void SetDateTime(TargetDateTime targetDateTime)
        {
            string time = targetDateTime.Hour + ":" + targetDateTime.Minute + ":" + targetDateTime.Second + ".00";
            string date = String.Empty;

            switch (dateFormat)
            {
                case DateFormats.DDMMYYYY:
                    date = targetDateTime.Day + "/" + targetDateTime.Month + "/" + targetDateTime.Year;
                    break;
                case DateFormats.MMDDYYYY:
                    date = targetDateTime.Month + "/" + targetDateTime.Day + "/" + targetDateTime.Year;
                    break;
                case DateFormats.YYYYMMDD:
                    date = targetDateTime.Year + "/" + targetDateTime.Month + "/" + targetDateTime.Day;
                    break;

            }

            Console.WriteLine("Modifying system time... Start game now.");

            // Sets system time every 100ms until FF3 is launched
            while (!GameRunning())
            {
                Process setTime = Process.Start("cmd.exe", "/C time " + time);
                Process setDate = Process.Start("cmd.exe", "/C date " + date);
                Thread.Sleep(100);
            }
            TimeZone.Instance.RevertTime();
        }

    }
}