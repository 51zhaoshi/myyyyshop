namespace Microsoft.Practices.EnterpriseLibrary.Data.Instrumentation
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation;
    using System;

    public abstract class DataEvent : BaseWmiEvent
    {
        private string instanceName;

        protected DataEvent(string instanceName)
        {
            this.instanceName = instanceName;
        }

        public string InstanceName
        {
            get
            {
                return this.instanceName;
            }
        }
    }
}

