namespace Maticsoft.Json.Conversion
{
    using System;
    using System.ComponentModel;
    using System.Globalization;

    [Serializable, AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class JsonMemberNamingConventionAttribute : Attribute, IPropertyDescriptorCustomization
    {
        private NamingConvention _convention;

        public JsonMemberNamingConventionAttribute()
        {
        }

        public JsonMemberNamingConventionAttribute(NamingConvention convention)
        {
            this._convention = convention;
        }

        void IPropertyDescriptorCustomization.Apply(PropertyDescriptor property)
        {
            if (property == null)
            {
                throw new ArgumentNullException("property");
            }
            string name = property.Name;
            switch (this.Convention)
            {
                case NamingConvention.Camel:
                    SetName(property, char.ToLower(name[0], CultureInfo.InvariantCulture) + name.Substring(1));
                    return;

                case NamingConvention.Pascal:
                    SetName(property, char.ToUpper(name[0], CultureInfo.InvariantCulture) + name.Substring(1));
                    return;

                case NamingConvention.Upper:
                    SetName(property, name.ToUpper(CultureInfo.InvariantCulture));
                    return;

                case NamingConvention.Lower:
                    SetName(property, name.ToLower(CultureInfo.InvariantCulture));
                    return;
            }
        }

        private static void SetName(PropertyDescriptor property, string name)
        {
            ((IPropertyCustomization) property).SetName(name);
        }

        public NamingConvention Convention
        {
            get
            {
                return this._convention;
            }
            set
            {
                this._convention = value;
            }
        }
    }
}

