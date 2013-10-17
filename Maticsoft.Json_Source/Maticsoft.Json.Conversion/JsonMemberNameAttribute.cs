namespace Maticsoft.Json.Conversion
{
    using Maticsoft.Json;
    using System;
    using System.ComponentModel;

    [Serializable, AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class JsonMemberNameAttribute : Attribute, IPropertyDescriptorCustomization
    {
        private string _name;

        public JsonMemberNameAttribute()
        {
        }

        public JsonMemberNameAttribute(string name)
        {
            this._name = name;
        }

        protected virtual void ApplyCustomization(PropertyDescriptor property)
        {
            if (property == null)
            {
                throw new ArgumentNullException("property");
            }
            if (this.Name.Length != 0)
            {
                ((IPropertyCustomization) property).SetName(this.Name);
            }
        }

        void IPropertyDescriptorCustomization.Apply(PropertyDescriptor property)
        {
            this.ApplyCustomization(property);
        }

        public string Name
        {
            get
            {
                return Mask.NullString(this._name);
            }
            set
            {
                this._name = value;
            }
        }
    }
}

