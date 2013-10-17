namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Storage
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using System;
    using System.Runtime.CompilerServices;

    public interface IConfigurationChangeWatcher : IDisposable
    {
        abstract event ConfigurationChangedEventHandler ConfigurationChanged;

        void StartWatching();
        void StopWatching();

        string SectionName { get; }
    }
}

