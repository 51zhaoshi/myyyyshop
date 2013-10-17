namespace Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation
{
    using System;
    using System.Management.Instrumentation;

    [InstrumentationClass(InstrumentationType.Event)]
    public abstract class BaseWmiEvent
    {
        private DateTime utcTimeStamp = DateTime.UtcNow;

        protected BaseWmiEvent()
        {
        }

        public DateTime UtcTimeStamp
        {
            get
            {
                return this.utcTimeStamp;
            }
        }
    }
}

