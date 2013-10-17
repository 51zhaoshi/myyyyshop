namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability
{
    using System;
    using System.Globalization;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct ImplementationKey
    {
        public string fileName;
        public string applicationName;
        public bool enableGroupPolicies;
        public ImplementationKey(string fileName, string applicationName, bool enableGroupPolicies)
        {
            this.fileName = (fileName != null) ? fileName.ToLower(CultureInfo.CurrentCulture) : null;
            this.applicationName = (applicationName != null) ? applicationName.ToLower(CultureInfo.CurrentCulture) : null;
            this.enableGroupPolicies = enableGroupPolicies;
        }
    }
}

