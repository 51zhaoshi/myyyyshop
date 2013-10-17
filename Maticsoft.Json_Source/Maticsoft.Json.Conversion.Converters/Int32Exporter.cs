namespace Maticsoft.Json.Conversion.Converters
{
    using System;
    using System.Globalization;

    public class Int32Exporter : NumberExporterBase
    {
        public Int32Exporter() : base(typeof(int))
        {
        }

        protected override string ConvertToString(object value)
        {
            int num = (int) value;
            return num.ToString(CultureInfo.InvariantCulture);
        }
    }
}

