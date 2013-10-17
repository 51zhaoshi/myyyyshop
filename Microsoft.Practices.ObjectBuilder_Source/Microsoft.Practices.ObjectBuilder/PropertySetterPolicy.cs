namespace Microsoft.Practices.ObjectBuilder
{
    using System;
    using System.Collections.Generic;

    public class PropertySetterPolicy : IPropertySetterPolicy, IBuilderPolicy
    {
        private Dictionary<string, IPropertySetterInfo> properties = new Dictionary<string, IPropertySetterInfo>();

        public Dictionary<string, IPropertySetterInfo> Properties
        {
            get
            {
                return this.properties;
            }
        }
    }
}

