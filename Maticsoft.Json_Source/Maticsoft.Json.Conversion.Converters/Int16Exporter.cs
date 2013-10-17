namespace Maticsoft.Json.Conversion.Converters
{
    using System;
    using System.Globalization;

    public class Int16Exporter : NumberExporterBase
    {
        public Int16Exporter() : base(typeof(short))
        {
        }

        protected override string ConvertToString(object value)
        {
            short num = (short) value;
            return num.ToString(CultureInfo.InvariantCulture);
        }
    }
}

