namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration
{
    using System;
    using System.ComponentModel;
    using System.Globalization;

    public class ByteArrayTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return (typeof(string) == sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return (typeof(byte[]) == destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return Convert.FromBase64String((string) value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            return Convert.ToBase64String((byte[]) value);
        }
    }
}

