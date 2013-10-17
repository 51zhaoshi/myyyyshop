namespace Maticsoft.Json.Conversion.Converters
{
    using System;
    using System.Globalization;

    public class ByteExporter : NumberExporterBase
    {
        public ByteExporter() : base(typeof(byte))
        {
        }

        protected override string ConvertToString(object value)
        {
            byte num = (byte) value;
            return num.ToString(CultureInfo.InvariantCulture);
        }
    }
}

