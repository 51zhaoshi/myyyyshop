namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.Properties;
    using System;

    public abstract class RegistryKeyBase
    {
        internal const string PolicyValueName = "Available";

        protected RegistryKeyBase()
        {
        }

        private static void CheckValidName(string name, string argumentName)
        {
            if (name == null)
            {
                throw new ArgumentNullException(argumentName);
            }
            if (name.Length == 0)
            {
                throw new ArgumentException(string.Format(Resources.Culture, Resources.ExceptionArgumentEmpty, new object[] { argumentName }));
            }
        }

        public abstract void Close();
        protected abstract object DoGetValue(string valueName);
        public abstract IRegistryKey DoOpenSubKey(string name);
        public bool? GetBoolValue(string valueName)
        {
            return new bool?(this.GetIntValue(valueName).Value == 1);
        }

        public T? GetEnumValue<T>(string valueName) where T: struct
        {
            T? nullable;
            string stringValue = this.GetStringValue(valueName);
            try
            {
                nullable = new T?((T) Enum.Parse(typeof(T), stringValue));
            }
            catch (ArgumentException)
            {
                throw new RegistryAccessException(string.Format(Resources.Culture, Resources.ExceptionRegistryValueNotEnumValue, new object[] { this.Name, valueName, typeof(T).Name, stringValue }));
            }
            return nullable;
        }

        public int? GetIntValue(string valueName)
        {
            int? nullable;
            object obj2 = this.GetValue(valueName);
            try
            {
                nullable = new int?((int) obj2);
            }
            catch (InvalidCastException)
            {
                throw new RegistryAccessException(string.Format(Resources.Culture, Resources.ExceptionRegistryValueOfWrongType, new object[] { this.Name, valueName, typeof(int).Name, obj2.GetType().Name }));
            }
            return nullable;
        }

        public string GetStringValue(string valueName)
        {
            string str;
            object obj2 = this.GetValue(valueName);
            try
            {
                str = (string) obj2;
            }
            catch (InvalidCastException)
            {
                throw new RegistryAccessException(string.Format(Resources.Culture, Resources.ExceptionRegistryValueOfWrongType, new object[] { this.Name, valueName, typeof(string).Name, obj2.GetType().Name }));
            }
            return str;
        }

        public Type GetTypeValue(string valueName)
        {
            string stringValue = this.GetStringValue(valueName);
            Type type = Type.GetType(stringValue, false);
            if (type == null)
            {
                throw new RegistryAccessException(string.Format(Resources.Culture, Resources.ExceptionRegistryValueNotTypeName, new object[] { this.Name, valueName, stringValue }));
            }
            return type;
        }

        private object GetValue(string valueName)
        {
            CheckValidName(valueName, "valueName");
            object obj2 = this.DoGetValue(valueName);
            if (obj2 == null)
            {
                throw new RegistryAccessException(string.Format(Resources.Culture, Resources.ExceptionMissingRegistryValue, new object[] { this.Name, valueName }));
            }
            return obj2;
        }

        public abstract string[] GetValueNames();
        public IRegistryKey OpenSubKey(string name)
        {
            CheckValidName(name, "name");
            return this.DoOpenSubKey(name);
        }

        public bool IsPolicyKey
        {
            get
            {
                return (this.DoGetValue("Available") != null);
            }
        }

        public abstract string Name { get; }
    }
}

