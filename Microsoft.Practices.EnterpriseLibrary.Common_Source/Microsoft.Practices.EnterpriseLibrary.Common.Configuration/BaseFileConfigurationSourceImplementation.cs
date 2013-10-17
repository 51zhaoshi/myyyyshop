namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Configuration;
    using System.Runtime.InteropServices;

    public abstract class BaseFileConfigurationSourceImplementation : IDisposable
    {
        private ConfigurationSourceWatcher configFileWatcher;
        private string configurationFilepath;
        private EventHandlerList eventHandlers;
        private object eventHandlersLock;
        private object lockMe;
        public const string NullConfigSource = "__null__";
        private bool refresh;
        private Dictionary<string, ConfigurationSourceWatcher> watchedConfigSourceMapping;
        private Dictionary<string, ConfigurationSourceWatcher> watchedSectionMapping;

        public BaseFileConfigurationSourceImplementation(string configurationFilepath)
        {
            this.lockMe = new object();
            this.refresh = true;
            this.eventHandlersLock = new object();
            this.eventHandlers = new EventHandlerList();
            this.configurationFilepath = configurationFilepath;
            this.watchedConfigSourceMapping = new Dictionary<string, ConfigurationSourceWatcher>();
            this.watchedSectionMapping = new Dictionary<string, ConfigurationSourceWatcher>();
        }

        public BaseFileConfigurationSourceImplementation(string configurationFilepath, bool refresh) : this(configurationFilepath)
        {
            this.refresh = refresh;
        }

        public void AddSectionChangeHandler(string sectionName, ConfigurationChangedEventHandler handler)
        {
            lock (this.eventHandlersLock)
            {
                this.eventHandlers.AddHandler(sectionName, handler);
            }
        }

        private void AddSectionsToUpdate(ConfigurationSourceWatcher watcher, IDictionary<string, string> sectionsToUpdate)
        {
            foreach (string str in watcher.WatchedSections)
            {
                sectionsToUpdate.Add(str, watcher.ConfigSource);
            }
        }

        public void ConfigSourceChanged(string configSource)
        {
            IDictionary<string, string> dictionary3;
            IDictionary<string, string> sectionsToUpdate = new Dictionary<string, string>();
            IDictionary<string, string> dictionary2 = new Dictionary<string, string>();
            ICollection<string> sectionsToNotify = new List<string>();
            lock (this.lockMe)
            {
                if (this.configFileWatcher != null)
                {
                    this.AddSectionsToUpdate(this.configFileWatcher, sectionsToUpdate);
                }
                foreach (ConfigurationSourceWatcher watcher in this.watchedConfigSourceMapping.Values)
                {
                    if (watcher != this.configFileWatcher)
                    {
                        this.AddSectionsToUpdate(watcher, dictionary2);
                    }
                }
            }
            this.RefreshAndValidateSections(sectionsToUpdate, dictionary2, out sectionsToNotify, out dictionary3);
            this.UpdateWatchersForSections(dictionary3);
            this.NotifyUpdatedSections(sectionsToNotify);
        }

        private ConfigurationSourceWatcher CreateWatcherForConfigSource(string configSource)
        {
            ConfigurationSourceWatcher watcher = null;
            if (string.Empty == configSource)
            {
                watcher = new ConfigurationFileSourceWatcher(this.configurationFilepath, configSource, this.refresh, new ConfigurationChangedEventHandler(this.OnConfigurationChanged));
                this.configFileWatcher = watcher;
            }
            else
            {
                watcher = new ConfigurationFileSourceWatcher(this.configurationFilepath, configSource, this.refresh && !"__null__".Equals(configSource), new ConfigurationChangedEventHandler(this.OnExternalConfigurationChanged));
            }
            this.watchedConfigSourceMapping.Add(configSource, watcher);
            return watcher;
        }

        public void Dispose()
        {
            foreach (IDisposable disposable in this.watchedConfigSourceMapping.Values)
            {
                disposable.Dispose();
            }
        }

        public void ExternalConfigSourceChanged(string configSource)
        {
            string[] strArray;
            lock (this.lockMe)
            {
                ConfigurationSourceWatcher watcher = null;
                this.watchedConfigSourceMapping.TryGetValue(configSource, out watcher);
                strArray = new string[watcher.WatchedSections.Count];
                watcher.WatchedSections.CopyTo(strArray, 0);
            }
            this.RefreshExternalSections(strArray);
            this.NotifyUpdatedSections(strArray);
        }

        public abstract ConfigurationSection GetSection(string sectionName);
        private bool IsWatchingConfigSource(string configSource)
        {
            return this.watchedConfigSourceMapping.ContainsKey(configSource);
        }

        private bool IsWatchingSection(string sectionName)
        {
            return this.watchedSectionMapping.ContainsKey(sectionName);
        }

        private void LinkWatcherForSection(ConfigurationSourceWatcher watcher, string sectionName)
        {
            this.watchedSectionMapping.Add(sectionName, watcher);
            watcher.WatchedSections.Add(sectionName);
        }

        private void NotifyUpdatedSections(IEnumerable<string> sectionsToNotify)
        {
            foreach (string str in sectionsToNotify)
            {
                Delegate[] invocationList = null;
                lock (this.eventHandlersLock)
                {
                    ConfigurationChangedEventHandler handler = (ConfigurationChangedEventHandler) this.eventHandlers[str];
                    if (handler == null)
                    {
                        continue;
                    }
                    invocationList = handler.GetInvocationList();
                }
                ConfigurationChangedEventArgs e = new ConfigurationChangedEventArgs(str);
                try
                {
                    foreach (ConfigurationChangedEventHandler handler2 in invocationList)
                    {
                        if (handler2 != null)
                        {
                            handler2(this, e);
                        }
                    }
                    continue;
                }
                catch
                {
                    continue;
                }
            }
        }

        private void OnConfigurationChanged(object sender, ConfigurationChangedEventArgs args)
        {
            this.ConfigSourceChanged(args.SectionName);
        }

        private void OnExternalConfigurationChanged(object sender, ConfigurationChangedEventArgs args)
        {
            this.ExternalConfigSourceChanged(args.SectionName);
        }

        protected abstract void RefreshAndValidateSections(IDictionary<string, string> localSectionsToRefresh, IDictionary<string, string> externalSectionsToRefresh, out ICollection<string> sectionsToNotify, out IDictionary<string, string> sectionsWithChangedConfigSource);
        protected abstract void RefreshExternalSections(string[] sectionsToRefresh);
        private void RemoveConfigSourceWatcher(ConfigurationSourceWatcher watcher)
        {
            this.watchedConfigSourceMapping.Remove(watcher.ConfigSource);
            ((IDisposable) watcher).Dispose();
        }

        public void RemoveSectionChangeHandler(string sectionName, ConfigurationChangedEventHandler handler)
        {
            lock (this.eventHandlersLock)
            {
                this.eventHandlers.RemoveHandler(sectionName, handler);
            }
        }

        protected void SetConfigurationWatchers(string sectionName, ConfigurationSection configurationSection)
        {
            if (configurationSection != null)
            {
                lock (this.lockMe)
                {
                    if (!this.IsWatchingSection(sectionName))
                    {
                        this.SetWatcherForSection(sectionName, configurationSection.SectionInformation.ConfigSource);
                    }
                }
            }
        }

        private void SetWatcherForSection(string sectionName, string configSource)
        {
            ConfigurationSourceWatcher watcher = null;
            this.watchedConfigSourceMapping.TryGetValue(configSource, out watcher);
            if (watcher == null)
            {
                watcher = this.CreateWatcherForConfigSource(configSource);
            }
            else
            {
                watcher.StopWatching();
            }
            this.LinkWatcherForSection(watcher, sectionName);
            watcher.StartWatching();
            if ((string.Empty != configSource) && !this.IsWatchingConfigSource(string.Empty))
            {
                this.CreateWatcherForConfigSource(string.Empty).StartWatching();
            }
        }

        private void UnlinkWatcherForSection(ConfigurationSourceWatcher watcher, string sectionName)
        {
            this.watchedSectionMapping.Remove(sectionName);
            watcher.WatchedSections.Remove(sectionName);
            if ((watcher.WatchedSections.Count == 0) && (this.configFileWatcher != watcher))
            {
                this.RemoveConfigSourceWatcher(watcher);
            }
        }

        private void UpdateWatcherForSection(string sectionName, string configSource)
        {
            ConfigurationSourceWatcher watcher = null;
            this.watchedSectionMapping.TryGetValue(sectionName, out watcher);
            if ((watcher == null) || (watcher.ConfigSource != configSource))
            {
                if (watcher != null)
                {
                    this.UnlinkWatcherForSection(watcher, sectionName);
                }
                if (configSource != null)
                {
                    this.SetWatcherForSection(sectionName, configSource);
                }
            }
        }

        private void UpdateWatchersForSections(IDictionary<string, string> sectionsChangingSource)
        {
            lock (this.lockMe)
            {
                foreach (KeyValuePair<string, string> pair in sectionsChangingSource)
                {
                    this.UpdateWatcherForSection(pair.Key, pair.Value);
                }
            }
        }

        internal IDictionary<string, ConfigurationSourceWatcher> ConfigSourceWatcherMappings
        {
            get
            {
                return this.watchedConfigSourceMapping;
            }
        }

        internal EventHandlerList SectionChangedHandlers
        {
            get
            {
                return this.eventHandlers;
            }
        }

        internal ICollection<string> WatchedConfigSources
        {
            get
            {
                return this.watchedConfigSourceMapping.Keys;
            }
        }

        internal ICollection<string> WatchedSections
        {
            get
            {
                return this.watchedSectionMapping.Keys;
            }
        }
    }
}

