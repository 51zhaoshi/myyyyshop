namespace Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation
{
    using System;

    [AttributeUsage(AttributeTargets.Method, Inherited=false, AllowMultiple=true)]
    public sealed class InstrumentationConsumerAttribute : InstrumentationBaseAttribute
    {
        public InstrumentationConsumerAttribute(string subjectName) : base(subjectName)
        {
        }
    }
}

