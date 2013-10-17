namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation;
    using System;

    internal class InstrumentationAttacherFactory
    {
        public IInstrumentationAttacher CreateBinder(object createdObject, object[] constructorArgs, ConfigurationReflectionCache reflectionCache)
        {
            InstrumentationListenerAttribute instrumentationListenerAttribute = this.GetInstrumentationListenerAttribute(createdObject, reflectionCache);
            if (instrumentationListenerAttribute == null)
            {
                return new NoBindingInstrumentationAttacher();
            }
            Type listenerType = instrumentationListenerAttribute.ListenerType;
            Type listenerBinderType = instrumentationListenerAttribute.ListenerBinderType;
            if (listenerBinderType == null)
            {
                return new ReflectionInstrumentationAttacher(createdObject, listenerType, constructorArgs);
            }
            return new ExplicitInstrumentationAttacher(createdObject, listenerType, constructorArgs, listenerBinderType);
        }

        private InstrumentationListenerAttribute GetInstrumentationListenerAttribute(object createdObject, ConfigurationReflectionCache reflectionCache)
        {
            Type type = createdObject.GetType();
            return reflectionCache.GetCustomAttribute<InstrumentationListenerAttribute>(type, true);
        }
    }
}

