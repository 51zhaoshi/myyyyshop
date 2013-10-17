namespace Maticsoft.Controls
{
    using System;
    using System.Collections;
    using System.ComponentModel;

    public class ThumbnailImageConverter : StringConverter
    {
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            ArrayList values = new ArrayList();
            values.Add(true);
            values.Add(false);
            return new TypeConverter.StandardValuesCollection(values);
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return false;
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
    }
}

