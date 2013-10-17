namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability
{
    using System;
    using System.Collections.Generic;

    internal class ImplementationKeyComparer : IEqualityComparer<ImplementationKey>
    {
        public bool Equals(ImplementationKey x, ImplementationKey y)
        {
            if ((x.fileName != y.fileName) && ((x.fileName == null) || !x.fileName.Equals(y.fileName, StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }
            if ((x.applicationName != y.applicationName) && ((x.applicationName == null) || !x.applicationName.Equals(y.applicationName, StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }
            if (x.enableGroupPolicies != y.enableGroupPolicies)
            {
                return false;
            }
            return true;
        }

        public int GetHashCode(ImplementationKey obj)
        {
            return ((((obj.fileName == null) ? 0 : obj.fileName.GetHashCode()) ^ ((obj.applicationName == null) ? 0 : obj.applicationName.GetHashCode())) ^ obj.enableGroupPolicies.GetHashCode());
        }
    }
}

