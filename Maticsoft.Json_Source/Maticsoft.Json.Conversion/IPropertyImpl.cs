namespace Maticsoft.Json.Conversion
{
    using System;

    public interface IPropertyImpl
    {
        object GetValue(object obj);
        void SetValue(object obj, object value);
    }
}

