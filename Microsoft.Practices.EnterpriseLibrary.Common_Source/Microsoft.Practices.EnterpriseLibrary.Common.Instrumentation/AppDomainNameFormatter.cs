namespace Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation
{
    using System;

    public class AppDomainNameFormatter : IPerformanceCounterNameFormatter
    {
        public string CreateName(string nameSuffix)
        {
            PerformanceCounterInstanceName name = new PerformanceCounterInstanceName(AppDomain.CurrentDomain.FriendlyName, nameSuffix);
            return name.ToString();
        }
    }
}

