namespace Maticsoft.Json.Conversion.Converters
{
    using System;
    using System.Globalization;

    public class SingleExporter : NumberExporterBase
    {
        public SingleExporter() : base(typeof(float))
        {
        }

        protected override string ConvertToString(object value)
        {
            float num = (float) value;
            return num.ToString(CultureInfo.InvariantCulture);
        }
    }
}

