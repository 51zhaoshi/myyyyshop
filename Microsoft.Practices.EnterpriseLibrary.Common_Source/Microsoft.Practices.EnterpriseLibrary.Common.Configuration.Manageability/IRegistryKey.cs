namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability
{
    using System;

    public interface IRegistryKey : IDisposable
    {
        void Close();
        bool? GetBoolValue(string valueName);
        T? GetEnumValue<T>(string valueName) where T: struct;
        int? GetIntValue(string valueName);
        string GetStringValue(string valueName);
        Type GetTypeValue(string valueName);
        string[] GetValueNames();
        IRegistryKey OpenSubKey(string name);

        bool IsPolicyKey { get; }

        string Name { get; }
    }
}

