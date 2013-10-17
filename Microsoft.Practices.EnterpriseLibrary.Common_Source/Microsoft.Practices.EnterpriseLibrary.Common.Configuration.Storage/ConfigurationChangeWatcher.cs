namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Storage
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Common.Properties;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Threading;

    public abstract class ConfigurationChangeWatcher : IConfigurationChangeWatcher, IDisposable
    {
        private static readonly object configurationChangedKey = new object();
        private static int defaultPollDelayInMilliseconds = 0x3a98;
        private EventHandlerList eventHandlers = new EventHandlerList();
        private DateTime lastWriteTime;
        private object lockObj = new object();
        private int pollDelayInMilliseconds = defaultPollDelayInMilliseconds;
        private PollingStatus pollingStatus;
        private Thread pollingThread;

        public event ConfigurationChangedEventHandler ConfigurationChanged
        {
            add
            {
                this.eventHandlers.AddHandler(configurationChangedKey, value);
            }
            remove
            {
                this.eventHandlers.RemoveHandler(configurationChangedKey, value);
            }
        }

        protected abstract ConfigurationChangedEventArgs BuildEventData();
        protected abstract string BuildThreadName();
        public void Dispose()
        {
            this.Disposing(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Disposing(bool isDisposing)
        {
            if (isDisposing)
            {
                this.eventHandlers.Dispose();
                this.StopWatching();
            }
        }

        ~ConfigurationChangeWatcher()
        {
            this.Disposing(false);
        }

        protected abstract DateTime GetCurrentLastWriteTime();
        protected abstract string GetEventSourceName();
        private void LogException(Exception e)
        {
            try
            {
                EventLog.WriteEntry(this.GetEventSourceName(), Resources.ExceptionEventRaisingFailed + base.GetType().FullName + " :" + e.Message, EventLogEntryType.Error);
            }
            catch
            {
            }
        }

        protected virtual void OnConfigurationChanged()
        {
            ConfigurationChangedEventHandler handler = (ConfigurationChangedEventHandler) this.eventHandlers[configurationChangedKey];
            ConfigurationChangedEventArgs e = this.BuildEventData();
            try
            {
                if (handler != null)
                {
                    foreach (ConfigurationChangedEventHandler handler2 in handler.GetInvocationList())
                    {
                        if (handler2 != null)
                        {
                            handler2(this, e);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                this.LogException(exception);
            }
        }

        private void Poller(object parameter)
        {
            this.lastWriteTime = DateTime.MinValue;
            DateTime minValue = DateTime.MinValue;
            PollingStatus status = (PollingStatus) parameter;
            while (status.Polling)
            {
                minValue = this.GetCurrentLastWriteTime();
                if (minValue != DateTime.MinValue)
                {
                    if (this.lastWriteTime.Equals(DateTime.MinValue))
                    {
                        this.lastWriteTime = minValue;
                    }
                    else if (!this.lastWriteTime.Equals(minValue))
                    {
                        this.lastWriteTime = minValue;
                        this.OnConfigurationChanged();
                    }
                }
                Thread.Sleep(this.pollDelayInMilliseconds);
            }
        }

        internal static void ResetDefaultPollDelay()
        {
            defaultPollDelayInMilliseconds = 0x3a98;
        }

        internal static void SetDefaultPollDelayInMilliseconds(int newDefaultPollDelayInMilliseconds)
        {
            defaultPollDelayInMilliseconds = newDefaultPollDelayInMilliseconds;
        }

        internal void SetPollDelayInMilliseconds(int newDelayInMilliseconds)
        {
            this.pollDelayInMilliseconds = newDelayInMilliseconds;
        }

        public void StartWatching()
        {
            lock (this.lockObj)
            {
                if (this.pollingThread == null)
                {
                    this.pollingStatus = new PollingStatus(true);
                    this.pollingThread = new Thread(new ParameterizedThreadStart(this.Poller));
                    this.pollingThread.IsBackground = true;
                    this.pollingThread.Name = this.BuildThreadName();
                    this.pollingThread.Start(this.pollingStatus);
                }
            }
        }

        public void StopWatching()
        {
            lock (this.lockObj)
            {
                if (this.pollingThread != null)
                {
                    this.pollingStatus.Polling = false;
                    this.pollingStatus = null;
                    this.pollingThread = null;
                }
            }
        }

        public abstract string SectionName { get; }

        private class PollingStatus
        {
            private bool polling;

            public PollingStatus(bool polling)
            {
                this.polling = polling;
            }

            public bool Polling
            {
                get
                {
                    return this.polling;
                }
                set
                {
                    this.polling = value;
                }
            }
        }
    }
}

