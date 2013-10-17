namespace Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation
{
    using System;

    public class NoPrefixNameFormatter : IPerformanceCounterNameFormatter
    {
        public string CreateName(string nameSuffix)
        {
            return new PerformanceCounterInstanceName("", nameSuffix).ToString();
        }
    }
}

