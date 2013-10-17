namespace Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    public class EnterpriseLibraryPerformanceCounterFactory
    {
        private Dictionary<string, PerformanceCounter> counterCache = new Dictionary<string, PerformanceCounter>();
        private object lockObject = new object();

        public void ClearCachedCounters()
        {
            this.counterCache.Clear();
        }

        public EnterpriseLibraryPerformanceCounter CreateCounter(string categoryName, string counterName, string[] instanceNames)
        {
            string str = categoryName.ToLowerInvariant() + counterName.ToLowerInvariant();
            PerformanceCounter[] counters = new PerformanceCounter[instanceNames.Length];
            for (int i = 0; i < instanceNames.Length; i++)
            {
                string key = str + instanceNames[i].ToLowerInvariant();
                lock (this.lockObject)
                {
                    if (!this.counterCache.ContainsKey(key))
                    {
                        PerformanceCounter counter = new PerformanceCounter(categoryName, counterName, instanceNames[i], false);
                        this.counterCache.Add(key, counter);
                    }
                    counters[i] = this.counterCache[key];
                }
            }
            return new EnterpriseLibraryPerformanceCounter(counters);
        }
    }
}

