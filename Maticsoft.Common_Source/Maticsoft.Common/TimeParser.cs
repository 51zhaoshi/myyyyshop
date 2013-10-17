namespace Maticsoft.Common
{
    using System;
    using System.Globalization;

    public class TimeParser
    {
        public static string DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            string str = null;
            try
            {
                TimeSpan span = (TimeSpan) (DateTime2 - DateTime1);
                if (span.Days >= 1)
                {
                    return (DateTime1.Month.ToString() + "月" + DateTime1.Day.ToString() + "日");
                }
                if (span.Hours > 1)
                {
                    return (span.Hours.ToString() + "小时前");
                }
                str = span.Minutes.ToString() + "分钟前";
            }
            catch
            {
            }
            return str;
        }

        public static string DateTimeFormat(object obj, string format, bool isFormat)
        {
            string str = string.Empty;
            if ((obj == null) || !PageValidate.IsDateTime(obj.ToString()))
            {
                return str;
            }
            if (isFormat)
            {
                return Convert.ToDateTime(obj).ToString(format);
            }
            return obj.ToString();
        }

        public static int GetMonthLastDate(int year, int month)
        {
            DateTime time = new DateTime(year, month, new GregorianCalendar().GetDaysInMonth(year, month));
            return time.Day;
        }

        public static string SecondToDateTime(int seconds)
        {
            TimeSpan span = new TimeSpan(0, 0, seconds);
            return string.Format("{0:00}:{1:00}:{2:00}", (int) span.TotalHours, span.Minutes, span.Seconds);
        }

        public static int SecondToMinute(int Second)
        {
            decimal d = Second / 60M;
            return Convert.ToInt32(Math.Ceiling(d));
        }

        public static int TimeToSecond(int hour, int minute, int second)
        {
            TimeSpan span = new TimeSpan(hour, minute, second);
            return (int) span.TotalSeconds;
        }
    }
}

