namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Storage
{
    using System;

    internal class ConfigurationChangeWatcherPauser : IDisposable
    {
        private readonly IConfigurationChangeWatcher watcher;

        public ConfigurationChangeWatcherPauser(IConfigurationChangeWatcher watcher)
        {
            this.watcher = watcher;
            if (watcher != null)
            {
                watcher.StopWatching();
            }
        }

        public void Dispose()
        {
            if (this.watcher != null)
            {
                this.watcher.StartWatching();
            }
        }
    }
}

