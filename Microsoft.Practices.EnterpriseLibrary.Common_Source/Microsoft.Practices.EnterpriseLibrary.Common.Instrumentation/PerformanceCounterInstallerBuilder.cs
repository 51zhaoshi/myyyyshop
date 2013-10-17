namespace Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Properties;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using System.Resources;

    public class PerformanceCounterInstallerBuilder : AbstractInstallerBuilder
    {
        public PerformanceCounterInstallerBuilder(Type[] availableTypes) : base(availableTypes, typeof(PerformanceCountersDefinitionAttribute))
        {
        }

        private void CollectPerformanceCounters(Type instrumentedType, PerformanceCounterInstaller installer)
        {
            foreach (FieldInfo info in instrumentedType.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance))
            {
                object[] customAttributes = info.GetCustomAttributes(typeof(PerformanceCounterAttribute), false);
                if (customAttributes.Length == 1)
                {
                    PerformanceCounterAttribute attribute = (PerformanceCounterAttribute) customAttributes[0];
                    CounterCreationData existingCounter = this.GetExistingCounter(installer, attribute.CounterName);
                    if (existingCounter == null)
                    {
                        installer.Counters.Add(new CounterCreationData(attribute.CounterName, GetCounterHelp(attribute.CounterHelp, instrumentedType.Assembly), attribute.CounterType));
                        if (attribute.HasBaseCounter())
                        {
                            installer.Counters.Add(new CounterCreationData(attribute.BaseCounterName, GetCounterHelp(attribute.BaseCounterHelp, instrumentedType.Assembly), attribute.BaseCounterType));
                        }
                    }
                    else if ((existingCounter.CounterType != attribute.CounterType) || !existingCounter.CounterHelp.Equals(GetCounterHelp(attribute.CounterHelp, instrumentedType.Assembly)))
                    {
                        throw new InvalidOperationException(string.Format(Resources.Culture, Resources.ExceptionPerformanceCounterRedefined, new object[] { existingCounter.CounterName, installer.CategoryName, instrumentedType.FullName }));
                    }
                }
            }
        }

        protected override ICollection<Installer> CreateInstallers(ICollection<Type> instrumentedTypes)
        {
            List<Installer> installers = new List<Installer>();
            foreach (Type type in instrumentedTypes)
            {
                PerformanceCounterInstaller orCreateInstaller = this.GetOrCreateInstaller(type, installers);
                this.CollectPerformanceCounters(type, orCreateInstaller);
            }
            return installers;
        }

        internal static string GetCategoryHelp(PerformanceCountersDefinitionAttribute attribute, Assembly originalAssembly)
        {
            return GetResourceString(attribute.CategoryHelp, originalAssembly);
        }

        internal static string GetCounterHelp(string resourceName, Assembly originalAssembly)
        {
            return GetResourceString(resourceName, originalAssembly);
        }

        private CounterCreationData GetExistingCounter(PerformanceCounterInstaller installer, string counterName)
        {
            foreach (CounterCreationData data in installer.Counters)
            {
                if (data.CounterName.Equals(counterName, StringComparison.CurrentCulture))
                {
                    return data;
                }
            }
            return null;
        }

        private PerformanceCounterInstaller GetExistingInstaller(string categoryName, IList<Installer> installers)
        {
            foreach (PerformanceCounterInstaller installer in installers)
            {
                if (installer.CategoryName.Equals(categoryName, StringComparison.CurrentCulture))
                {
                    return installer;
                }
            }
            return null;
        }

        private PerformanceCounterInstaller GetOrCreateInstaller(Type instrumentedType, IList<Installer> installers)
        {
            PerformanceCountersDefinitionAttribute attribute = (PerformanceCountersDefinitionAttribute) instrumentedType.GetCustomAttributes(typeof(PerformanceCountersDefinitionAttribute), false)[0];
            PerformanceCounterInstaller existingInstaller = this.GetExistingInstaller(attribute.CategoryName, installers);
            if (existingInstaller == null)
            {
                existingInstaller = new PerformanceCounterInstaller();
                this.PopulateCounterCategoryData(attribute, instrumentedType.Assembly, existingInstaller);
                installers.Add(existingInstaller);
            }
            return existingInstaller;
        }

        private static string GetResourceString(string name, Assembly originalAssembly)
        {
            string str = null;
            string[] manifestResourceNames = originalAssembly.GetManifestResourceNames();
            for (int i = 0; i < manifestResourceNames.Length; i++)
            {
                try
                {
                    int startIndex = manifestResourceNames[i].LastIndexOf(".resources");
                    str = new ResourceManager(manifestResourceNames[i].Remove(startIndex), originalAssembly).GetString(name);
                }
                catch (Exception)
                {
                }
                if (!string.IsNullOrEmpty(str))
                {
                    return str;
                }
            }
            return "";
        }

        private void PopulateCounterCategoryData(PerformanceCountersDefinitionAttribute attribute, Assembly originalAssembly, PerformanceCounterInstaller installer)
        {
            installer.CategoryName = attribute.CategoryName;
            installer.CategoryHelp = GetCategoryHelp(attribute, originalAssembly);
            installer.CategoryType = attribute.CategoryType;
        }
    }
}

