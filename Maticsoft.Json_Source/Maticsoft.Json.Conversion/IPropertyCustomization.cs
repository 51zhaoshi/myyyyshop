namespace Maticsoft.Json.Conversion
{
    using System;

    public interface IPropertyCustomization
    {
        IPropertyImpl OverrideImpl(IPropertyImpl impl);
        void SetName(string name);
        void SetType(Type type);
    }
}

