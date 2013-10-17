namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Storage;
    using System;
    using System.Collections.Generic;

    public abstract class ConfigurationSourceWatcher : IDisposable
    {
        private string configurationSource;
        protected ConfigurationChangeWatcher configWatcher;
        private IList<string> watchedSections;

        public ConfigurationSourceWatcher(string configSource, bool refresh, ConfigurationChangedEventHandler changed)
        {
            this.configurationSource = configSource;
            this.watchedSections = new List<string>();
        }

        public void StartWatching()
        {
            if (this.configWatcher != null)
            {
                this.configWatcher.StartWatching();
            }
        }

        public void StopWatching()
        {
            if (this.configWatcher != null)
            {
                this.configWatcher.StopWatching();
            }
        }

        void IDisposable.Dispose()
        {
            if (this.configWatcher != null)
            {
                this.configWatcher.Dispose();
            }
        }

        public string ConfigSource
        {
            get
            {
                return this.configurationSource;
            }
            set
            {
                this.configurationSource = value;
            }
        }

        public IList<string> WatchedSections
        {
            get
            {
                return this.watchedSections;
            }
            set
            {
                this.watchedSections = value;
            }
        }

        public ConfigurationChangeWatcher Watcher
        {
            get
            {
                return this.configWatcher;
            }
        }
    }
}

