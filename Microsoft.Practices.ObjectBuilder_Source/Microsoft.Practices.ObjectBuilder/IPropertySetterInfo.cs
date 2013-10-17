namespace Microsoft.Practices.ObjectBuilder
{
    using System;
    using System.Reflection;

    public interface IPropertySetterInfo
    {
        object GetValue(IBuilderContext context, Type type, string id, PropertyInfo propInfo);
        PropertyInfo SelectProperty(IBuilderContext context, Type type, string id);
    }
}

