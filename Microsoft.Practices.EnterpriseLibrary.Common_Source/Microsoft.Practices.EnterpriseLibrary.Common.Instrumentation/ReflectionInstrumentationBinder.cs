namespace Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class ReflectionInstrumentationBinder
    {
        public void Bind(object eventSource, object eventListener)
        {
            EventBinder binder = new EventBinder(eventSource, eventListener);
            Dictionary<string, List<EventInfo>> instrumentationEvents = this.GetInstrumentationEvents(eventSource);
            Dictionary<string, List<MethodInfo>> instrumentationListeners = this.GetInstrumentationListeners(eventListener);
            foreach (string str in instrumentationEvents.Keys)
            {
                if (instrumentationListeners.ContainsKey(str))
                {
                    List<EventInfo> list = instrumentationEvents[str];
                    List<MethodInfo> list2 = instrumentationListeners[str];
                    foreach (EventInfo info in list)
                    {
                        foreach (MethodInfo info2 in list2)
                        {
                            binder.Bind(info, info2);
                        }
                    }
                    continue;
                }
            }
        }

        private Dictionary<string, List<TMemberInfoType>> GetAttributedMembers<TMemberInfoType, TAttributeType>(object target, MemberFinder<TMemberInfoType> memberFinder) where TMemberInfoType: MemberInfo where TAttributeType: InstrumentationBaseAttribute
        {
            Dictionary<string, List<TMemberInfoType>> dictionary = new Dictionary<string, List<TMemberInfoType>>();
            Type t = target.GetType();
            MemberInfo[] infoArray = (MemberInfo[]) memberFinder(t);
            foreach (MemberInfo info in infoArray)
            {
                foreach (InstrumentationBaseAttribute attribute in info.GetCustomAttributes(typeof(TAttributeType), false))
                {
                    if (!dictionary.ContainsKey(attribute.SubjectName))
                    {
                        dictionary.Add(attribute.SubjectName, new List<TMemberInfoType>());
                    }
                    dictionary[attribute.SubjectName].Add((TMemberInfoType) info);
                }
            }
            return dictionary;
        }

        private EventInfo[] GetEventInfo(Type t)
        {
            return t.GetEvents();
        }

        private Dictionary<string, List<EventInfo>> GetInstrumentationEvents(object eventSource)
        {
            return this.GetAttributedMembers<EventInfo, InstrumentationProviderAttribute>(eventSource, t => this.GetEventInfo(t));
        }

        private Dictionary<string, List<MethodInfo>> GetInstrumentationListeners(object eventListener)
        {
            return this.GetAttributedMembers<MethodInfo, InstrumentationConsumerAttribute>(eventListener, t => this.GetMethodInfo(t));
        }

        private MethodInfo[] GetMethodInfo(Type t)
        {
            return t.GetMethods();
        }

        private delegate T[] MemberFinder<T>(Type t);
    }
}

