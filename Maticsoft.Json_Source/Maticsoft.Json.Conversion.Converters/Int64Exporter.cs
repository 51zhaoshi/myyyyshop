namespace Maticsoft.Json.Conversion.Converters
{
    using System;
    using System.Globalization;

    public class Int64Exporter : NumberExporterBase
    {
        public Int64Exporter() : base(typeof(long))
        {
        }

        protected override string ConvertToString(object value)
        {
            long num = (long) value;
            return num.ToString(CultureInfo.InvariantCulture);
        }
    }
}

