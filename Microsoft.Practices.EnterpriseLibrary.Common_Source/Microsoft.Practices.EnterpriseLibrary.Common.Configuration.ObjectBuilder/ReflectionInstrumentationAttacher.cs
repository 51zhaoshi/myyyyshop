namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation;
    using System;

    public class ReflectionInstrumentationAttacher : IInstrumentationAttacher
    {
        private object[] listenerConstructorArgs;
        private Type listenerType;
        private object source;

        public ReflectionInstrumentationAttacher(object source, Type listenerType, object[] listenerConstructorArgs)
        {
            this.source = source;
            this.listenerType = listenerType;
            this.listenerConstructorArgs = listenerConstructorArgs;
        }

        public void BindInstrumentation()
        {
            object listener = this.CreateListener();
            this.BindSourceToListener(this.source, listener);
        }

        private void BindSourceToListener(object createdObject, object listener)
        {
            new ReflectionInstrumentationBinder().Bind(createdObject, listener);
        }

        private object CreateListener()
        {
            return Activator.CreateInstance(this.listenerType, this.listenerConstructorArgs);
        }
    }
}

