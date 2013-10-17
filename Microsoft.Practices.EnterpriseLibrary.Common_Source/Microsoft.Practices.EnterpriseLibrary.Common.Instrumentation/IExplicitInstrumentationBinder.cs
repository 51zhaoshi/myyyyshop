namespace Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation
{
    using System;

    public interface IExplicitInstrumentationBinder
    {
        void Bind(object source, object listener);
    }
}

