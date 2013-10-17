namespace Maticsoft.Json.Conversion.Converters
{
    using System;
    using System.Globalization;

    public class DoubleExporter : NumberExporterBase
    {
        public DoubleExporter() : base(typeof(double))
        {
        }

        protected override string ConvertToString(object value)
        {
            double num = (double) value;
            return num.ToString(CultureInfo.InvariantCulture);
        }
    }
}

