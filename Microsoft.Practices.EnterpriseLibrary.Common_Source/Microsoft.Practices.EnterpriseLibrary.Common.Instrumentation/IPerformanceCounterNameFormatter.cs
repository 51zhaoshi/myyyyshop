namespace Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation
{
    using System;

    public interface IPerformanceCounterNameFormatter
    {
        string CreateName(string nameSuffix);
    }
}

