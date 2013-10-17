namespace Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation
{
    using System;

    [AttributeUsage(AttributeTargets.Class, Inherited=true)]
    public sealed class InstrumentationListenerAttribute : Attribute
    {
        private Type listenerBinderType;
        private Type listenerType;

        public InstrumentationListenerAttribute(Type listenerType)
        {
            this.listenerType = listenerType;
            this.listenerBinderType = null;
        }

        public InstrumentationListenerAttribute(Type listenerType, Type listenerBinderType)
        {
            this.listenerType = listenerType;
            this.listenerBinderType = listenerBinderType;
        }

        public Type ListenerBinderType
        {
            get
            {
                return this.listenerBinderType;
            }
        }

        public Type ListenerType
        {
            get
            {
                return this.listenerType;
            }
        }
    }
}

