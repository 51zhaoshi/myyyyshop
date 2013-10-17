namespace Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation
{
    using System;

    [AttributeUsage(AttributeTargets.Event, Inherited=false, AllowMultiple=false)]
    public sealed class InstrumentationProviderAttribute : InstrumentationBaseAttribute
    {
        public InstrumentationProviderAttribute(string subjectName) : base(subjectName)
        {
        }
    }
}

