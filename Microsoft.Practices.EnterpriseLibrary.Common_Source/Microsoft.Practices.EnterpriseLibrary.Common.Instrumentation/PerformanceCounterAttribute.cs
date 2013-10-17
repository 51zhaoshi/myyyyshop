namespace Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation
{
    using System;
    using System.Diagnostics;

    [AttributeUsage(AttributeTargets.Field)]
    public sealed class PerformanceCounterAttribute : Attribute
    {
        private string baseCounterHelp;
        private string baseCounterName;
        private PerformanceCounterType baseCounterType;
        private string counterHelp;
        private string counterName;
        private PerformanceCounterType counterType;

        public PerformanceCounterAttribute(string counterName, string counterHelp, PerformanceCounterType counterType)
        {
            this.counterName = counterName;
            this.counterHelp = counterHelp;
            this.counterType = counterType;
        }

        public bool HasBaseCounter()
        {
            return (this.baseCounterName != null);
        }

        public string BaseCounterHelp
        {
            get
            {
                return this.baseCounterHelp;
            }
            set
            {
                this.baseCounterHelp = value;
            }
        }

        public string BaseCounterName
        {
            get
            {
                return this.baseCounterName;
            }
            set
            {
                this.baseCounterName = value;
            }
        }

        public PerformanceCounterType BaseCounterType
        {
            get
            {
                return this.baseCounterType;
            }
            set
            {
                this.baseCounterType = value;
            }
        }

        public string CounterHelp
        {
            get
            {
                return this.counterHelp;
            }
        }

        public string CounterName
        {
            get
            {
                return this.counterName;
            }
        }

        public PerformanceCounterType CounterType
        {
            get
            {
                return this.counterType;
            }
        }
    }
}

