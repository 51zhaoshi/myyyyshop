namespace Maticsoft.Json.Conversion
{
    using System;
    using System.ComponentModel;

    public interface IPropertyDescriptorCustomization
    {
        void Apply(PropertyDescriptor property);
    }
}

