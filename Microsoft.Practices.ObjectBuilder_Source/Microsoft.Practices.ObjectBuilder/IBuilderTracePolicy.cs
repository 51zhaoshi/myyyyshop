namespace Microsoft.Practices.ObjectBuilder
{
    using System;

    public interface IBuilderTracePolicy : IBuilderPolicy
    {
        void Trace(string format, params object[] args);
    }
}

