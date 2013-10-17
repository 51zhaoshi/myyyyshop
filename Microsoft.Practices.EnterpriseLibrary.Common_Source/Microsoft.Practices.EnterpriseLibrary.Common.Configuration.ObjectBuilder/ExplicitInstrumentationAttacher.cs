namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation;
    using System;

    public class ExplicitInstrumentationAttacher : IInstrumentationAttacher
    {
        private Type explicitBinderType;
        private object[] listenerConstructorArguments;
        private Type listenerType;
        private object source;

        public ExplicitInstrumentationAttacher(object source, Type listenerType, object[] listenerConstructorArguments, Type explicitBinderType)
        {
            this.source = source;
            this.listenerType = listenerType;
            this.listenerConstructorArguments = listenerConstructorArguments;
            this.explicitBinderType = explicitBinderType;
        }

        public void BindInstrumentation()
        {
            IExplicitInstrumentationBinder binder = (IExplicitInstrumentationBinder) Activator.CreateInstance(this.explicitBinderType);
            object listener = Activator.CreateInstance(this.listenerType, this.listenerConstructorArguments);
            binder.Bind(this.source, listener);
        }
    }
}

