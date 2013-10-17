namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.Properties;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    internal class ConfigurationChangeNotificationCoordinator
    {
        private EventHandlerList eventHandlers = new EventHandlerList();
        private object eventHandlersLock = new object();

        public void AddSectionChangeHandler(string sectionName, ConfigurationChangedEventHandler handler)
        {
            lock (this.eventHandlersLock)
            {
                this.eventHandlers.AddHandler(this.CanonicalizeSectionName(sectionName), handler);
            }
        }

        private string CanonicalizeSectionName(string sectionName)
        {
            return string.Intern(sectionName);
        }

        public void NotifyUpdatedSections(IEnumerable<string> sectionsToNotify)
        {
            foreach (string str in sectionsToNotify)
            {
                string sectionName = this.CanonicalizeSectionName(str);
                Delegate[] invocationList = null;
                lock (this.eventHandlersLock)
                {
                    ConfigurationChangedEventHandler handler = (ConfigurationChangedEventHandler) this.eventHandlers[sectionName];
                    if (handler == null)
                    {
                        continue;
                    }
                    invocationList = handler.GetInvocationList();
                }
                ConfigurationChangedEventArgs e = new ConfigurationChangedEventArgs(sectionName);
                foreach (ConfigurationChangedEventHandler handler2 in invocationList)
                {
                    try
                    {
                        if (handler2 != null)
                        {
                            handler2(this, e);
                        }
                    }
                    catch (Exception exception)
                    {
                        ManageabilityExtensionsLogger.LogException(exception, string.Format(Resources.Culture, Resources.ExceptionErrorOnCallbackForSectionUpdate, new object[] { sectionName, handler2.ToString() }));
                    }
                }
            }
        }

        public void RemoveSectionChangeHandler(string sectionName, ConfigurationChangedEventHandler handler)
        {
            lock (this.eventHandlersLock)
            {
                this.eventHandlers.RemoveHandler(this.CanonicalizeSectionName(sectionName), handler);
            }
        }
    }
}

