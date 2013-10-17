namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Storage;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;

    internal class ConfigurationChangeWatcherCoordinator : IDisposable
    {
        private Dictionary<string, ConfigurationChangeFileWatcher> configSourceWatcherMapping;
        private string mainConfigurationFileName;
        private string mainConfigurationFilePath;
        public const string MainConfigurationFileSource = "";
        private bool refresh;

        public event ConfigurationChangedEventHandler ConfigurationChanged;

        public ConfigurationChangeWatcherCoordinator(string mainConfigurationFileName, bool refresh)
        {
            this.mainConfigurationFileName = mainConfigurationFileName;
            this.mainConfigurationFilePath = Path.GetDirectoryName(mainConfigurationFileName);
            this.refresh = refresh;
            this.configSourceWatcherMapping = new Dictionary<string, ConfigurationChangeFileWatcher>();
            this.CreateWatcherForConfigSource("");
        }

        private ConfigurationChangeFileWatcher CreateWatcherForConfigSource(string configSource)
        {
            ConfigurationChangeFileWatcher watcher = null;
            if ("".Equals(configSource))
            {
                watcher = new ConfigurationChangeFileWatcher(this.mainConfigurationFileName, configSource);
            }
            else
            {
                watcher = new ConfigurationChangeFileWatcher(Path.Combine(this.mainConfigurationFilePath, configSource), configSource);
            }
            watcher.ConfigurationChanged += new ConfigurationChangedEventHandler(this.OnConfigurationChanged);
            this.configSourceWatcherMapping.Add(configSource, watcher);
            if (this.refresh)
            {
                watcher.StartWatching();
            }
            return watcher;
        }

        public void Dispose()
        {
            foreach (IDisposable disposable in this.configSourceWatcherMapping.Values)
            {
                disposable.Dispose();
            }
        }

        public bool IsWatchingConfigSource(string configSource)
        {
            return this.configSourceWatcherMapping.ContainsKey(configSource);
        }

        internal void OnConfigurationChanged(object sender, ConfigurationChangedEventArgs args)
        {
            if (this.ConfigurationChanged != null)
            {
                this.ConfigurationChanged(this, args);
            }
        }

        public void RemoveWatcherForConfigSource(string configSource)
        {
            ConfigurationChangeFileWatcher watcher = null;
            this.configSourceWatcherMapping.TryGetValue(configSource, out watcher);
            if (watcher != null)
            {
                this.configSourceWatcherMapping.Remove(configSource);
                watcher.Dispose();
            }
        }

        public void SetWatcherForConfigSource(string configSource)
        {
            if (!this.IsWatchingConfigSource(configSource))
            {
                this.CreateWatcherForConfigSource(configSource);
            }
        }

        internal IDictionary<string, ConfigurationChangeFileWatcher> ConfigSourceWatcherMapping
        {
            get
            {
                return this.configSourceWatcherMapping;
            }
        }

        internal ICollection<string> WatchedConfigSources
        {
            get
            {
                return this.configSourceWatcherMapping.Keys;
            }
        }
    }
}

