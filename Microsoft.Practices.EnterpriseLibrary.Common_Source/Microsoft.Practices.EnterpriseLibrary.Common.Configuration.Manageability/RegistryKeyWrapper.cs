namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability
{
    using Microsoft.Win32;
    using System;

    internal class RegistryKeyWrapper : RegistryKeyBase, IRegistryKey, IDisposable
    {
        private RegistryKey registryKey;

        public RegistryKeyWrapper(RegistryKey registryKey)
        {
            this.registryKey = registryKey;
        }

        public override void Close()
        {
            this.registryKey.Close();
        }

        public void Dispose()
        {
            this.Close();
        }

        protected override object DoGetValue(string name)
        {
            return this.registryKey.GetValue(name);
        }

        public override IRegistryKey DoOpenSubKey(string name)
        {
            RegistryKey registryKey = this.registryKey.OpenSubKey(name, RegistryKeyPermissionCheck.ReadSubTree);
            if (registryKey != null)
            {
                return new RegistryKeyWrapper(registryKey);
            }
            return null;
        }

        public override string[] GetValueNames()
        {
            return this.registryKey.GetValueNames();
        }

        public override string Name
        {
            get
            {
                return this.registryKey.Name;
            }
        }
    }
}

