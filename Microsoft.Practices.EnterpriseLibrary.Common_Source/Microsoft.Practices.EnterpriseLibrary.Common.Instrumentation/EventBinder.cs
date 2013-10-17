namespace Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation
{
    using System;
    using System.Reflection;

    public class EventBinder
    {
        private object listener;
        private object source;

        public EventBinder(object source, object listener)
        {
            this.source = source;
            this.listener = listener;
        }

        public virtual void Bind(EventInfo sourceEvent, MethodInfo listenerMethod)
        {
            Delegate handler = Delegate.CreateDelegate(sourceEvent.EventHandlerType, this.listener, listenerMethod);
            sourceEvent.AddEventHandler(this.source, handler);
        }
    }
}

