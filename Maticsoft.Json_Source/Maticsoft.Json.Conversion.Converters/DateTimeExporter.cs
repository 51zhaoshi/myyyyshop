namespace Maticsoft.Json.Conversion.Converters
{
    using Maticsoft.Json;
    using Maticsoft.Json.Conversion;
    using System;

    public class DateTimeExporter : ExporterBase
    {
        public DateTimeExporter() : base(typeof(DateTime))
        {
        }

        private static void Digits2(char[] buffer, int value, int offset)
        {
            buffer[offset++] = (char) (0x30 + (value / 10));
            buffer[offset] = (char) (0x30 + (value % 10));
        }

        private static void Digits3(char[] buffer, int value, int offset)
        {
            buffer[offset++] = (char) (0x30 + (value / 100));
            Digits2(buffer, value % 100, offset);
        }

        private static void Digits4(char[] buffer, int value, int offset)
        {
            buffer[offset++] = (char) (0x30 + (value / 0x3e8));
            Digits3(buffer, value % 0x3e8, offset);
        }

        private static void Digits7(char[] buffer, int value, int offset)
        {
            Digits4(buffer, value / 0x3e8, offset);
            Digits3(buffer, value % 0x3e8, offset + 4);
        }

        protected virtual void ExportTime(DateTime time, JsonWriter writer)
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }
            writer.WriteString(FormatDateTime(time));
        }

        protected override void ExportValue(ExportContext context, object value, JsonWriter writer)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }
            this.ExportTime((DateTime) value, writer);
        }

        private static char[] FormatDateTime(DateTime when)
        {
            char ch2;
            char[] buffer = new char["yyyy-MM-ddTHH:mm:ss.fffffffzzzzzz".Length];
            buffer[4] = buffer[7] = '-';
            buffer[10] = 'T';
            buffer[30] = ch2 = ':';
            buffer[13] = buffer[0x10] = ch2;
            buffer[0x13] = '.';
            Digits4(buffer, when.Year, 0);
            Digits2(buffer, when.Month, 5);
            Digits2(buffer, when.Day, 8);
            Digits2(buffer, when.Hour, 11);
            Digits2(buffer, when.Minute, 14);
            Digits2(buffer, when.Second, 0x11);
            Digits7(buffer, (int) (when.Ticks % 0x989680L), 20);
            TimeSpan utcOffset = TimeZone.CurrentTimeZone.GetUtcOffset(when);
            buffer[0x1b] = (utcOffset.Ticks >= 0L) ? '+' : '-';
            Digits2(buffer, utcOffset.Hours, 0x1c);
            Digits2(buffer, utcOffset.Minutes, 0x1f);
            return buffer;
        }
    }
}

