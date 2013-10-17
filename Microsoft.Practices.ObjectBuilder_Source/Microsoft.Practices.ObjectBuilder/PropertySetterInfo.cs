namespace Microsoft.Practices.ObjectBuilder
{
    using System;
    using System.Reflection;

    public class PropertySetterInfo : IPropertySetterInfo
    {
        private string name;
        private PropertyInfo prop;
        private IParameter value;

        public PropertySetterInfo(PropertyInfo propInfo, IParameter value)
        {
            this.prop = propInfo;
            this.value = value;
        }

        public PropertySetterInfo(string name, IParameter value)
        {
            this.name = name;
            this.value = value;
        }

        public object GetValue(IBuilderContext context, Type type, string id, PropertyInfo propInfo)
        {
            return this.value.GetValue(context);
        }

        public PropertyInfo SelectProperty(IBuilderContext context, Type type, string id)
        {
            if (this.prop != null)
            {
                return this.prop;
            }
            return type.GetProperty(this.name);
        }
    }
}

