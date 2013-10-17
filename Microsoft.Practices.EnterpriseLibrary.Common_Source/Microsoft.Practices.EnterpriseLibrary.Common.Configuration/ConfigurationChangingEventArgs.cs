namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration
{
    using System;

    [Serializable]
    public class ConfigurationChangingEventArgs : ConfigurationChangedEventArgs
    {
        private bool cancel;
        private readonly object newValue;
        private readonly object oldValue;

        public ConfigurationChangingEventArgs(string sectionName, object oldValue, object newValue) : base(sectionName)
        {
            this.oldValue = oldValue;
            this.newValue = newValue;
        }

        public bool Cancel
        {
            get
            {
                return this.cancel;
            }
            set
            {
                this.cancel = value;
            }
        }

        public object NewValue
        {
            get
            {
                return this.newValue;
            }
        }

        public object OldValue
        {
            get
            {
                return this.oldValue;
            }
        }
    }
}

