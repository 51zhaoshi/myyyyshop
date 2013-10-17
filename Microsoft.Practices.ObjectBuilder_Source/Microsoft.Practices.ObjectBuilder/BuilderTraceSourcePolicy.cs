namespace Microsoft.Practices.ObjectBuilder
{
    using System;
    using System.Diagnostics;

    public class BuilderTraceSourcePolicy : IBuilderTracePolicy, IBuilderPolicy
    {
        private TraceSource traceSource;

        public BuilderTraceSourcePolicy(TraceSource traceSource)
        {
            this.traceSource = traceSource;
        }

        public void Trace(string format, params object[] args)
        {
            this.traceSource.TraceInformation(format, args);
        }
    }
}

