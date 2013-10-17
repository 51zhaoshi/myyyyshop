namespace Maticsoft.Json.Conversion
{
    using Maticsoft.Json;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Globalization;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public sealed class CustomTypeDescriptor : ICustomTypeDescriptor
    {
        private readonly PropertyDescriptorCollection _properties;

        public CustomTypeDescriptor(Type type) : this(type, null)
        {
        }

        public CustomTypeDescriptor(Type type, MemberInfo[] members) : this(type, members, null)
        {
        }

        public CustomTypeDescriptor(Type type, MemberInfo[] members, string[] names) : this(type, LikeAnonymousClass(type), members, names)
        {
        }

        private CustomTypeDescriptor(Type type, bool isAnonymousClass, MemberInfo[] members, string[] names)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            if (members == null)
            {
                FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);
                PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                ArrayList list = new ArrayList(fields.Length + properties.Length);
                list.AddRange(fields);
                list.AddRange(properties);
                for (int i = 0; i < list.Count; i++)
                {
                    MemberInfo info = (MemberInfo) list[i];
                    if (info.IsDefined(typeof(JsonIgnoreAttribute), true))
                    {
                        list.RemoveAt(i--);
                    }
                }
                members = (MemberInfo[]) list.ToArray(typeof(MemberInfo));
            }
            PropertyDescriptorCollection descriptors = new PropertyDescriptorCollection(null);
            int index = 0;
            foreach (MemberInfo info2 in members)
            {
                FieldInfo field = info2 as FieldInfo;
                string name = ((names != null) && (index < names.Length)) ? names[index] : null;
                TypeMemberDescriptor descriptor = null;
                if (field != null)
                {
                    if ((field.DeclaringType != type) && (field.ReflectedType != type))
                    {
                        throw new ArgumentException(null, "members");
                    }
                    if (!field.IsInitOnly && !field.IsLiteral)
                    {
                        descriptor = new TypeFieldDescriptor(field, name);
                    }
                }
                else
                {
                    PropertyInfo property = info2 as PropertyInfo;
                    if (property == null)
                    {
                        throw new ArgumentException(null, "members");
                    }
                    if ((property.DeclaringType != type) && (property.ReflectedType != type))
                    {
                        throw new ArgumentException(null, "members");
                    }
                    if ((property.CanRead && ((isAnonymousClass || property.CanWrite) || property.IsDefined(typeof(JsonExportAttribute), true))) && (property.GetIndexParameters().Length == 0))
                    {
                        descriptor = new TypePropertyDescriptor(property, isAnonymousClass ? Mask.EmptyString(name, property.Name) : name);
                    }
                }
                if (descriptor != null)
                {
                    descriptor.ApplyCustomizations();
                    descriptors.Add(descriptor);
                }
                index++;
            }
            this._properties = descriptors;
        }

        public static PropertyDescriptor CreateProperty(FieldInfo field)
        {
            if (field == null)
            {
                throw new ArgumentNullException("field");
            }
            return new TypeFieldDescriptor(field, field.Name);
        }

        public static PropertyDescriptor CreateProperty(PropertyInfo property)
        {
            if (property == null)
            {
                throw new ArgumentNullException("property");
            }
            return new TypePropertyDescriptor(property, property.Name);
        }

        public AttributeCollection GetAttributes()
        {
            return AttributeCollection.Empty;
        }

        public string GetClassName()
        {
            return null;
        }

        public string GetComponentName()
        {
            return null;
        }

        public TypeConverter GetConverter()
        {
            return new TypeConverter();
        }

        public EventDescriptor GetDefaultEvent()
        {
            return null;
        }

        public PropertyDescriptor GetDefaultProperty()
        {
            return null;
        }

        public object GetEditor(Type editorBaseType)
        {
            return null;
        }

        public EventDescriptorCollection GetEvents()
        {
            return EventDescriptorCollection.Empty;
        }

        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return EventDescriptorCollection.Empty;
        }

        public PropertyDescriptorCollection GetProperties()
        {
            return this._properties;
        }

        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            throw new NotImplementedException();
        }

        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return null;
        }

        internal static bool LikeAnonymousClass(Type type)
        {
            return ((((type.IsNotPublic && type.IsClass) && (type.IsSealed && (type.GetConstructor(Type.EmptyTypes) == null))) && type.IsGenericType) && type.IsDefined(typeof(CompilerGeneratedAttribute), false));
        }

        public static Maticsoft.Json.Conversion.CustomTypeDescriptor TryCreateForAnonymousClass(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            bool isAnonymousClass = LikeAnonymousClass(type);
            if (!isAnonymousClass)
            {
                return null;
            }
            return new Maticsoft.Json.Conversion.CustomTypeDescriptor(type, isAnonymousClass, null, null);
        }

        private sealed class TypeFieldDescriptor : Maticsoft.Json.Conversion.CustomTypeDescriptor.TypeMemberDescriptor
        {
            private readonly FieldInfo _field;

            public TypeFieldDescriptor(FieldInfo field, string name) : base(field, name, field.FieldType)
            {
                this._field = field;
            }

            protected override object GetValueImpl(object component)
            {
                return this._field.GetValue(component);
            }

            protected override void SetValueImpl(object component, object value)
            {
                this._field.SetValue(component, value);
                this.OnValueChanged(component, EventArgs.Empty);
            }

            public override bool IsReadOnly
            {
                get
                {
                    return this._field.IsInitOnly;
                }
            }

            protected override MemberInfo Member
            {
                get
                {
                    return this._field;
                }
            }
        }

        private abstract class TypeMemberDescriptor : PropertyDescriptor, IPropertyImpl, IPropertyCustomization, IServiceContainer, IServiceProvider
        {
            private string _customName;
            private int _customNameHashCode;
            private IPropertyImpl _impl;
            private Type _propertyType;
            private ServiceContainer _services;

            protected TypeMemberDescriptor(MemberInfo member, string name, Type propertyType) : base(ChooseName(name, member.Name), null)
            {
                this._impl = this;
                this._propertyType = propertyType;
            }

            internal void ApplyCustomizations()
            {
                IPropertyDescriptorCustomization[] customAttributes = (IPropertyDescriptorCustomization[]) this.Member.GetCustomAttributes(typeof(IPropertyDescriptorCustomization), true);
                if (customAttributes != null)
                {
                    foreach (IPropertyDescriptorCustomization customization in customAttributes)
                    {
                        customization.Apply(this);
                    }
                }
            }

            public override bool CanResetValue(object component)
            {
                return false;
            }

            private static string ChooseName(string propsedName, string baseName)
            {
                if (Mask.NullString(propsedName).Length > 0)
                {
                    return propsedName;
                }
                return ToCamelCase(baseName);
            }

            public override bool Equals(object obj)
            {
                Maticsoft.Json.Conversion.CustomTypeDescriptor.TypeMemberDescriptor descriptor = obj as Maticsoft.Json.Conversion.CustomTypeDescriptor.TypeMemberDescriptor;
                return ((descriptor != null) && descriptor.Member.Equals(this.Member));
            }

            public override int GetHashCode()
            {
                return this.Member.GetHashCode();
            }

            public override object GetValue(object component)
            {
                return this._impl.GetValue(component);
            }

            protected abstract object GetValueImpl(object component);
            IPropertyImpl IPropertyCustomization.OverrideImpl(IPropertyImpl impl)
            {
                if (impl == null)
                {
                    throw new ArgumentNullException("impl");
                }
                IPropertyImpl impl2 = this._impl;
                this._impl = impl;
                return impl2;
            }

            void IPropertyCustomization.SetName(string name)
            {
                if (name == null)
                {
                    throw new ArgumentNullException("name");
                }
                if (name.Length == 0)
                {
                    throw new ArgumentException(null, "name");
                }
                this._customName = name;
                this._customNameHashCode = name.GetHashCode();
            }

            void IPropertyCustomization.SetType(Type type)
            {
                if (type == null)
                {
                    throw new ArgumentNullException("type");
                }
                this._propertyType = type;
            }

            object IPropertyImpl.GetValue(object obj)
            {
                return this.GetValueImpl(obj);
            }

            void IPropertyImpl.SetValue(object obj, object value)
            {
                this.SetValueImpl(obj, value);
            }

            public override void ResetValue(object component)
            {
            }

            public override void SetValue(object component, object value)
            {
                if (this.IsReadOnly)
                {
                    throw new NotSupportedException();
                }
                this._impl.SetValue(component, value);
            }

            protected abstract void SetValueImpl(object component, object value);
            public override bool ShouldSerializeValue(object component)
            {
                return true;
            }

            void IServiceContainer.AddService(Type serviceType, ServiceCreatorCallback callback)
            {
                this.Services.AddService(serviceType, callback);
            }

            void IServiceContainer.AddService(Type serviceType, object serviceInstance)
            {
                this.Services.AddService(serviceType, serviceInstance);
            }

            void IServiceContainer.AddService(Type serviceType, ServiceCreatorCallback callback, bool promote)
            {
                this.Services.AddService(serviceType, callback, promote);
            }

            void IServiceContainer.AddService(Type serviceType, object serviceInstance, bool promote)
            {
                this.Services.AddService(serviceType, serviceInstance, promote);
            }

            void IServiceContainer.RemoveService(Type serviceType)
            {
                if (this._services != null)
                {
                    this._services.RemoveService(serviceType);
                }
            }

            void IServiceContainer.RemoveService(Type serviceType, bool promote)
            {
                if (this._services != null)
                {
                    this._services.RemoveService(serviceType, promote);
                }
            }

            object IServiceProvider.GetService(Type serviceType)
            {
                if (this._services == null)
                {
                    return null;
                }
                return this._services.GetService(serviceType);
            }

            private static string ToCamelCase(string s)
            {
                if ((s != null) && (s.Length != 0))
                {
                    return (char.ToLower(s[0], CultureInfo.InvariantCulture) + s.Substring(1));
                }
                return s;
            }

            public override Type ComponentType
            {
                get
                {
                    return this.Member.DeclaringType;
                }
            }

            protected abstract MemberInfo Member { get; }

            public override string Name
            {
                get
                {
                    if (this._customName == null)
                    {
                        return base.Name;
                    }
                    return this._customName;
                }
            }

            protected override int NameHashCode
            {
                get
                {
                    if (this._customName == null)
                    {
                        return base.NameHashCode;
                    }
                    return this._customNameHashCode;
                }
            }

            public override Type PropertyType
            {
                get
                {
                    return this._propertyType;
                }
            }

            private ServiceContainer Services
            {
                get
                {
                    if (this._services == null)
                    {
                        this._services = new ServiceContainer();
                    }
                    return this._services;
                }
            }
        }

        private sealed class TypePropertyDescriptor : Maticsoft.Json.Conversion.CustomTypeDescriptor.TypeMemberDescriptor
        {
            private readonly PropertyInfo _property;

            public TypePropertyDescriptor(PropertyInfo property, string name) : base(property, name, property.PropertyType)
            {
                this._property = property;
            }

            protected override object GetValueImpl(object component)
            {
                return this._property.GetValue(component, null);
            }

            protected override void SetValueImpl(object component, object value)
            {
                this._property.SetValue(component, value, null);
                this.OnValueChanged(component, EventArgs.Empty);
            }

            public override bool IsReadOnly
            {
                get
                {
                    return !this._property.CanWrite;
                }
            }

            protected override MemberInfo Member
            {
                get
                {
                    return this._property;
                }
            }
        }
    }
}

