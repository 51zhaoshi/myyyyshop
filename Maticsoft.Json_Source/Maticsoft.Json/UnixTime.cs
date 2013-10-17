namespace Maticsoft.Json
{
    using System;

    public sealed class UnixTime
    {
        public static readonly DateTime EpochUtc = new DateTime(0x7b2, 1, 1);

        private UnixTime()
        {
            throw new NotSupportedException();
        }

        public static DateTime ToDateTime(double time)
        {
            return ToDateTime((long) time, (int) Math.Round((double) ((time % 1.0) * 1000.0)));
        }

        public static DateTime ToDateTime(long time)
        {
            return ToDateTime(time, 0);
        }

        public static DateTime ToDateTime(long time, int ms)
        {
            if ((ms < 0) || (ms > 0x3e7))
            {
                throw new ArgumentOutOfRangeException("ms");
            }
            return EpochUtc.AddSeconds((double) time).AddMilliseconds((double) ms).ToLocalTime();
        }

        public static double ToDouble(DateTime time)
        {
            TimeSpan span = (TimeSpan) (time.ToUniversalTime() - new DateTime(0x7b2, 1, 1));
            return span.TotalSeconds;
        }

        public static long ToInt64(DateTime time)
        {
            return (long) ToDouble(time);
        }
    }
}

