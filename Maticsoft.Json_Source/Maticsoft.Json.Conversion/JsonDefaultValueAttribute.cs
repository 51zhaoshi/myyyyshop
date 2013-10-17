namespace Maticsoft.Json.Conversion
{
    using Maticsoft.Json;
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design;

    [Serializable, AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class JsonDefaultValueAttribute : Attribute, IPropertyDescriptorCustomization
    {
        private object _value;

        public JsonDefaultValueAttribute(bool value) : this(value)
        {
        }

        public JsonDefaultValueAttribute(byte value) : this(value)
        {
        }

        public JsonDefaultValueAttribute(char value) : this(value)
        {
        }

        public JsonDefaultValueAttribute(double value) : this(value)
        {
        }

        public JsonDefaultValueAttribute(short value) : this(value)
        {
        }

        public JsonDefaultValueAttribute(int value) : this(value)
        {
        }

        public JsonDefaultValueAttribute(long value) : this(value)
        {
        }

        public JsonDefaultValueAttribute(object value)
        {
            this._value = value;
        }

        public JsonDefaultValueAttribute(float value) : this(value)
        {
        }

        public JsonDefaultValueAttribute(string value) : this(value)
        {
        }

        public JsonDefaultValueAttribute(Type type, string value) : this(TypeDescriptor.GetConverter(type).ConvertFromInvariantString(value))
        {
        }

        void IPropertyDescriptorCustomization.Apply(PropertyDescriptor property)
        {
            if (property == null)
            {
                throw new ArgumentNullException("property");
            }
            if (this.Value != null)
            {
                ((IServiceContainer) property).AddService(typeof(IObjectMemberExporter), new PropertyExporter(property, this.Value));
            }
        }

        public object Value
        {
            get
            {
                return this._value;
            }
            set
            {
                this._value = value;
            }
        }

        private sealed class PropertyExporter : IObjectMemberExporter
        {
            private readonly object _defaultValue;
            private readonly PropertyDescriptor _property;

            public PropertyExporter(PropertyDescriptor property, object defaultValue)
            {
                this._property = property;
                this._defaultValue = defaultValue;
            }

            public void Export(ExportContext context, JsonWriter writer, object source)
            {
                if (context == null)
                {
                    throw new ArgumentNullException("context");
                }
                if (writer == null)
                {
                    throw new ArgumentNullException("writer");
                }
                if (source == null)
                {
                    throw new ArgumentNullException("source");
                }
                object o = this._property.GetValue(source);
                if (!JsonNull.LogicallyEquals(o) && !o.Equals(this._defaultValue))
                {
                    writer.WriteMember(this._property.Name);
                    context.Export(o, writer);
                }
            }
        }
    }
}

