namespace Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    public class EventLogInstallerBuilder : AbstractInstallerBuilder
    {
        public EventLogInstallerBuilder(Type[] potentialTypes) : base(potentialTypes, typeof(EventLogDefinitionAttribute))
        {
        }

        protected override ICollection<Installer> CreateInstallers(ICollection<Type> instrumentedTypes)
        {
            IList<Installer> list = new List<Installer>();
            foreach (Type type in instrumentedTypes)
            {
                EventLogDefinitionAttribute attribute = (EventLogDefinitionAttribute) type.GetCustomAttributes(typeof(EventLogDefinitionAttribute), false)[0];
                EventLogInstaller item = new EventLogInstaller {
                    Log = attribute.LogName,
                    Source = attribute.SourceName,
                    CategoryCount = attribute.CategoryCount
                };
                if (attribute.CategoryResourceFile != null)
                {
                    item.CategoryResourceFile = attribute.CategoryResourceFile;
                }
                if (attribute.MessageResourceFile != null)
                {
                    item.MessageResourceFile = attribute.MessageResourceFile;
                }
                if (attribute.ParameterResourceFile != null)
                {
                    item.ParameterResourceFile = attribute.ParameterResourceFile;
                }
                list.Add(item);
            }
            return list;
        }
    }
}

