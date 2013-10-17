namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability
{
    using System;
    using System.Runtime.CompilerServices;

    internal interface IGroupPolicyWatcher : IDisposable
    {
        abstract event GroupPolicyUpdateDelegate GroupPolicyUpdated;

        void StartWatching();
        void StopWatching();
    }
}

