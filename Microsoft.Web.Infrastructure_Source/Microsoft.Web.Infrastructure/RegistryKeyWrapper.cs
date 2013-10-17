namespace Microsoft.Web.Infrastructure
{
    using Microsoft.Win32;
    using System;
    using System.Security;

    [SecurityCritical]
    internal sealed class RegistryKeyWrapper : IRegistryKey, IDisposable
    {
        private readonly RegistryKey _registryKey;

        public RegistryKeyWrapper(RegistryKey registryKey)
        {
            this._registryKey = registryKey;
        }

        [SecuritySafeCritical]
        public void Dispose()
        {
            this._registryKey.Dispose();
        }

        [SecurityCritical]
        public string[] GetSubKeyNames()
        {
            return this._registryKey.GetSubKeyNames();
        }

        [SecurityCritical]
        public object GetValue(string name)
        {
            return this._registryKey.GetValue(name);
        }

        [SecurityCritical]
        public IRegistryKey OpenSubKey(string name)
        {
            RegistryKey registryKey = this._registryKey.OpenSubKey(name);
            if (registryKey == null)
            {
                return null;
            }
            return new RegistryKeyWrapper(registryKey);
        }

        [SecuritySafeCritical]
        public override string ToString()
        {
            return this._registryKey.ToString();
        }
    }
}

