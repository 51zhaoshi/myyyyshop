namespace Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation
{
    using System;
    using System.Diagnostics;

    public class EnterpriseLibraryPerformanceCounter
    {
        private string counterCategoryName;
        private string counterName;
        private PerformanceCounter[] counters;
        private string[] instanceNames;

        public EnterpriseLibraryPerformanceCounter(params PerformanceCounter[] counters)
        {
            this.counters = counters;
        }

        public EnterpriseLibraryPerformanceCounter(string counterCategoryName, string counterName) : this(counterCategoryName, counterName, new string[] { "Total" })
        {
        }

        public EnterpriseLibraryPerformanceCounter(string counterCategoryName, string counterName, params string[] instanceNames)
        {
            this.instanceNames = instanceNames;
            this.counterName = counterName;
            this.counterCategoryName = counterCategoryName;
            this.counters = new PerformanceCounter[instanceNames.Length];
            for (int i = 0; i < this.counters.Length; i++)
            {
                this.counters[i] = this.InstantiateCounter(instanceNames[i]);
            }
        }

        public void Clear()
        {
            foreach (PerformanceCounter counter in this.counters)
            {
                counter.RawValue = 0L;
            }
        }

        public long GetValueFor(string instanceName)
        {
            foreach (PerformanceCounter counter in this.counters)
            {
                if (counter.InstanceName.Equals(instanceName))
                {
                    return counter.RawValue;
                }
            }
            return -1L;
        }

        public void Increment()
        {
            foreach (PerformanceCounter counter in this.counters)
            {
                counter.Increment();
            }
        }

        public void IncrementBy(long value)
        {
            foreach (PerformanceCounter counter in this.counters)
            {
                counter.IncrementBy(value);
            }
        }

        protected PerformanceCounter InstantiateCounter(string instanceName)
        {
            return new PerformanceCounter(this.counterCategoryName, this.counterName, instanceName, false);
        }

        public void SetValueFor(string instanceName, long value)
        {
            foreach (PerformanceCounter counter in this.counters)
            {
                if (counter.InstanceName.Equals(instanceName))
                {
                    counter.RawValue = value;
                    return;
                }
            }
        }

        public PerformanceCounter[] Counters
        {
            get
            {
                return this.counters;
            }
        }

        public long Value
        {
            get
            {
                return this.counters[0].RawValue;
            }
        }
    }
}

