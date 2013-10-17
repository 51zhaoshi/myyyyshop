namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability
{
    using Microsoft.Win32;
    using System;

    internal class RegistryAccessor : IRegistryAccessor
    {
        public IRegistryKey CurrentUser
        {
            get
            {
                return new RegistryKeyWrapper(Registry.CurrentUser);
            }
        }

        public IRegistryKey LocalMachine
        {
            get
            {
                return new RegistryKeyWrapper(Registry.LocalMachine);
            }
        }
    }
}

