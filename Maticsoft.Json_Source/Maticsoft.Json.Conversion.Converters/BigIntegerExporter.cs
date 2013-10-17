namespace Maticsoft.Json.Conversion.Converters
{
    using System;
    using System.Globalization;
    using System.Numerics;

    public class BigIntegerExporter : NumberExporterBase
    {
        public BigIntegerExporter() : base(typeof(BigInteger))
        {
        }

        protected override string ConvertToString(object value)
        {
            BigInteger integer = (BigInteger) value;
            return integer.ToString(CultureInfo.InvariantCulture);
        }
    }
}

